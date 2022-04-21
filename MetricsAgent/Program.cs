using MetricsAgent.Jobs;
using FluentMigrator.Runner;
using NLog;
using NLog.Web;
using System.Data.SQLite;
using MetricsAgent.Repositoryes;
using MetricsAgent.Interfaces;
using AutoMapper;
using Quartz.Spi;
using Quartz;
using Quartz.Impl;
using MetricsAgent;

var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("InitMain");

try
{
    logger.Debug("init main");

    var serviceProvider = CreateServices();
    var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
    var mapper = mapperConfiguration.CreateMapper();

    builder.Services.AddControllers();
    builder.Services.AddHostedService<QuartzHostedService>();

    builder.Services.AddSingleton<ICPUMetricsRepository, CPUMetricsRepository>();
    builder.Services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
    builder.Services.AddSingleton<IHddMetricsRepository, HDDMetricsRepository>();
    builder.Services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
    builder.Services.AddSingleton<IRamMetricsRepository, RAMMetricsRepository>();
    builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
    builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
    builder.Services.AddSingleton<CpuMetricJob>();
    builder.Services.AddSingleton<RamMetricJob>();
    builder.Services.AddSingleton(new JobSchedule(jobType: typeof(CpuMetricJob),
                                                  cronExpression: "0/5 * * * * ?")); // Запускать каждые 5 секунд
    builder.Services.AddSingleton(new JobSchedule(jobType: typeof(RamMetricJob),
                                                  cronExpression: "0/5 * * * * ?"));

    builder.Services.AddSingleton(mapper);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    using (var scope = serviceProvider.CreateScope())
    {
        UpdateDatabase(scope.ServiceProvider);
    }

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
//YYYY-MM-DDTHH:MM
void CreateTable(string connectionString)
{
    //string connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    string[] tables = { "cpumetrics", "hddmetrics", "rammetrics", "networkmetrics", "dotnetmetrics" };
    using (var connection = new SQLiteConnection(connectionString))
    {
        connection.Open();
        using (var command = new SQLiteCommand(connection))
        {
            for (int i = 0; i < tables.Length; i++)
            {
                command.CommandText = $"DROP TABLE IF EXISTS {tables[i]}";
                command.ExecuteNonQuery();
                command.CommandText = $@"CREATE TABLE {tables[i]}(id INTEGER PRIMARY KEY, value INT, datetime TEXT)";
                command.ExecuteNonQuery();
                for (int j = 0; j < 10; j++)
                {
                    command.CommandText = $"INSERT INTO {tables[i]}(value, datetime)VALUES({(j + 1) * 10}, \'{DateTime.Now.ToString("s")}\')";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

static void UpdateDatabase(IServiceProvider serviceProvider)
{
    // Instantiate the runner
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

    // Execute the migrations
    runner.MigrateUp();
}
static IServiceProvider CreateServices()
{
    var sc = 
        // Add common FluentMigrator services
            // Add SQLite support to FluentMigrator
            // Set the connection string
            // Define the assembly containing the migrations
        // Enable logging to console in the FluentMigrator way
        // Build the service provider
     new ServiceCollection()
        .AddFluentMigratorCore()
        .ConfigureRunner(rb => rb
            .AddSQLite()
            .WithGlobalConnectionString("Data Source=test.db")
            .ScanIn(typeof(Program).Assembly).For.Migrations())
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        .BuildServiceProvider(false);
    return sc;
}
