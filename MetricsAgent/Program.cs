using NLog;
using NLog.Web;
using System.Data.SQLite;
using MetricsAgent.Repositoryes;
using MetricsAgent.Interfaces;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("InitMain");

try
{
    logger.Debug("init main");

    var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
    var mapper = mapperConfiguration.CreateMapper();

    builder.Services.AddControllers();
    builder.Services.AddScoped<ICPUMetricsRepository, CPUMetricsRepository>();
    builder.Services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
    builder.Services.AddScoped<IHddMetricsRepository, HDDMetricsRepository>();
    builder.Services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
    builder.Services.AddScoped<IRamMetricsRepository, RAMMetricsRepository>();
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

    CreateTable(app.Configuration.GetConnectionString("SQLiteDB"));

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
