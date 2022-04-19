using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Repositoryes;

public class DotNetMetricsRepository : IDotNetMetricsRepository
{
    private readonly string _connectionString;
    public DotNetMetricsRepository()
    {
        _connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    }
    //public DotNetMetricsRepository(IConfiguration configuration)
    //{
    //    _connectionString = configuration.GetConnectionString("SQLiteDB");
    //}

    #region Create

    public void Create(DotNetMetrics item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"INSERT INTO dotnetmetrics(value, datetime)VALUES({item.Value},\'{item.Time}\')";
                command.ExecuteNonQuery();
            }
        }
    }

    #endregion

    #region Read

    // dotnetmetrics
    public List<DotNetMetrics> GetAll()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "SELECT * FROM dotnetmetrics;";
                var result = new List<DotNetMetrics>();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new()
                        {
                            Id = reader.GetInt32(0),
                            Value = reader.GetInt32(1),
                            Time = reader.GetDateTime(2)
                        });
                    }
                    return result;
                }
            }
        }
    }

    public DotNetMetrics GetById(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"SELECT id,value,datetime FROM dotnetmetrics WHERE id = {id}";
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    return reader.Read() ? new()
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetDateTime(2)
                    } : null!;
                }
            }
        }
    }

    public List<DotNetMetrics> GetByTimeFilter(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"SELECT * FROM dotnetmetrics WHERE datetime BETWEEN \"{from.ToString("s")}\" AND \"{to.ToString("s")}\";";
                var result = new List<DotNetMetrics>();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(new()
                            {
                                Id = reader.GetInt32(0),
                                Value = reader.GetInt32(1),
                                Time = reader.GetDateTime(2)
                            });
                        }
                    }
                    return result;
                }
            }
        }
    }


    #endregion

    #region Update

    public void Update(DotNetMetrics item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"UPDATE dotnetmetrics SET value = {item.Value}, time =\'{item.Time}\' WHERE id = @id')";
                command.ExecuteNonQuery();
            }
        }
    }

    #endregion

    #region Delete

    public void Delete(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"DELETE FROM dotnetmetrics WHERE id={id}";
                command.ExecuteNonQuery();
            }
        }
    }

    #endregion
}
