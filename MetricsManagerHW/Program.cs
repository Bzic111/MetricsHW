using MetricsManagerHW.DAL.Client;
using AutoMapper;
using FluentMigrator.Runner;
using MetricsManagerHW.DAL.Repository;
using MetricsManagerHW.Interface;
using NLog;
using NLog.Web;
using Polly;
using Quartz.Spi;
using Quartz;
using Quartz.Impl;

var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    logger.Debug("init main");

    var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
    var mapper = mapperConfiguration.CreateMapper();

    builder.Services.AddControllers();

    builder.Services.AddSingleton<ICPUMetricsRepository, CPUMetricsRepository>();
    builder.Services.AddSingleton<IHddMetricsRepository, HDDMetricsRepository>();
    builder.Services.AddSingleton<IRamMetricsRepository, RAMMetricsRepository>();
    builder.Services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
    builder.Services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();

    builder.Services.AddFluentMigratorCore()
                    .ConfigureRunner(rb => rb
                        .AddSQLite()
                        .WithGlobalConnectionString("Data Source=test.db")
                        .ScanIn(typeof(Program).Assembly).For.Migrations())
                    .AddLogging(lb => lb.AddFluentMigratorConsole());

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpClient();
    builder.Services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>().AddTransientHttpErrorPolicy(p =>p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
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
