using AutoMapper;
using Dapper;
using FluentMigrator.Runner;
using MetricsManagerHW.DAL;
using MetricsManagerHW.DAL.Client;
using MetricsManagerHW.DAL.Repository;
using MetricsManagerHW.Interface;
using MetricsManagerHW.Jobs;
using NLog;
using NLog.Web;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
//http://localhost:5056
try
{
    logger.Debug("init main");

    var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
    IMapper mapper = mapperConfiguration.CreateMapper();

    builder.Services.AddSingleton(mapper);

    SqlMapper.AddTypeHandler(new DateTimeHandler());

    builder.Services.AddControllers();
    builder.Services.AddFluentMigratorCore()
                        .ConfigureRunner(rb => rb
                            .AddSQLite()
                            .WithGlobalConnectionString("SQLiteDB")
                            .ScanIn(typeof(Program).Assembly).For.Migrations())
                        .AddLogging(lb => lb.AddFluentMigratorConsole());

    // не забыть добавить методы для SELECT * FROM table ORDER BY column DESC LIMIT 1;

    builder.Services.AddSingleton<AgentsRepository>();
    builder.Services.AddSingleton<ICPUMetricsRepository, CPUMetricsRepository>();
    builder.Services.AddSingleton<IHddMetricsRepository, HDDMetricsRepository>();
    builder.Services.AddSingleton<IRamMetricsRepository, RAMMetricsRepository>();
    builder.Services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
    builder.Services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
    builder.Services.AddHostedService<QuartzHostedService>();

    builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
    builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

    builder.Services.AddSingleton<CpuMetricJob>();
    builder.Services.AddSingleton<HddMetricJob>();
    builder.Services.AddSingleton<RamMetricJob>();
    builder.Services.AddSingleton<DotNetMetricJob>();
    builder.Services.AddSingleton<NetworkMetricJob>();

    builder.Services.AddSingleton(new JobSchedule(jobType: typeof(CpuMetricJob),
                                                  cronExpression: "0/10 * * * * ?")); // Запускать каждые 5 секунд
    builder.Services.AddSingleton(new JobSchedule(jobType: typeof(RamMetricJob),
                                                  cronExpression: "0/10 * * * * ?"));
    builder.Services.AddSingleton(new JobSchedule(jobType: typeof(NetworkMetricJob),
                                                  cronExpression: "0/10 * * * * ?"));
    builder.Services.AddSingleton(new JobSchedule(jobType: typeof(HddMetricJob),
                                                  cronExpression: "0/10 * * * * ?"));
    builder.Services.AddSingleton(new JobSchedule(jobType: typeof(DotNetMetricJob),
                                                  cronExpression: "0/10 * * * * ?"));

    builder.Services.AddHttpClient<MetricsAgentClient>().AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    //.SetHandlerLifetime(TimeSpan.FromMinutes(5)).AddPolicyHandler(GetRetryPolicy());
    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        scope.ServiceProvider.GetRequiredService<IMigrationRunner>().MigrateUp();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, $"Stopped program because of exception:\n{ex.Message}");
    throw;
}
finally
{
    LogManager.Shutdown();
}
