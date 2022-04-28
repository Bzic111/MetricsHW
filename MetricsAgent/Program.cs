using MetricsAgent.Jobs;
using FluentMigrator.Runner;
using NLog;
using NLog.Web;
using MetricsAgent.Interfaces;
using AutoMapper;
using Quartz.Spi;
using Quartz;
using Quartz.Impl;
using MetricsAgent;
using MetricsAgent.DAL.Repositoryes;

var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    logger.Debug("init main");

    var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
    var mapper = mapperConfiguration.CreateMapper();

    builder.Services.AddControllers();

    builder.Services.AddHostedService<QuartzHostedService>();

    builder.Services.AddFluentMigratorCore()
                    .ConfigureRunner(rb => rb
                        .AddSQLite()
                        .WithGlobalConnectionString("Data Source=test.db")
                        .ScanIn(typeof(Program).Assembly).For.Migrations())
                    .AddLogging(lb => lb.AddFluentMigratorConsole());

    builder.Services.AddSingleton<ICPUMetricsRepository, CPUMetricsRepository>();
    builder.Services.AddSingleton<IHddMetricsRepository, HDDMetricsRepository>();
    builder.Services.AddSingleton<IRamMetricsRepository, RAMMetricsRepository>();
    builder.Services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
    builder.Services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();

    builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
    builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

    builder.Services.AddSingleton<CpuMetricJob>();
    builder.Services.AddSingleton<HddMetricJob>();
    builder.Services.AddSingleton<RamMetricJob>();
    builder.Services.AddSingleton<DotNetMetricJob>();
    builder.Services.AddSingleton<NetworkMetricJob>();

    builder.Services.AddSingleton(new JobSchedule(jobType: typeof(CpuMetricJob),
                                                  cronExpression: "0/5 * * * * ?")); // ��������� ������ 5 ������
    builder.Services.AddSingleton(new JobSchedule(jobType: typeof(RamMetricJob),
                                                  cronExpression: "0/5 * * * * ?"));
    builder.Services.AddSingleton(new JobSchedule(jobType: typeof(NetworkMetricJob),
                                                  cronExpression: "0/5 * * * * ?"));
    builder.Services.AddSingleton(new JobSchedule(jobType: typeof(HddMetricJob),
                                                  cronExpression: "0/5 * * * * ?"));
    builder.Services.AddSingleton(new JobSchedule(jobType: typeof(DotNetMetricJob),
                                                  cronExpression: "0/5 * * * * ?"));

    builder.Services.AddSingleton(mapper);

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();

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
