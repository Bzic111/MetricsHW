using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Repositoryes;

public class NetworkMetricsRepository : IRepository<NetworkMetrics>
{
    private readonly string _connectionString;

    public NetworkMetricsRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SQLiteDB");
    }

    #region Create

    public void Create(NetworkMetrics item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"INSERT INTO networkmetrics(value, datetime)VALUES({item.Value},\'{item.Time}\')";
                command.ExecuteNonQuery();
            }
        }
    }

    #endregion

    #region Read

    public IList<NetworkMetrics> GetAll()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "SELECT * FROM networkmetrics;";
                var result = new List<NetworkMetrics>();
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

    public NetworkMetrics GetById(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"SELECT id,value,datetime FROM networkmetrics WHERE id = {id}";
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

    public IList<NetworkMetrics> GetByTimeFilter(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"SELECT * FROM networkmetrics WHERE datetime BETWEEN \"{from.ToString("s")}\" AND \"{to.ToString("s")}\";";
                var result = new List<NetworkMetrics>();
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

    public void Update(NetworkMetrics item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"UPDATE networkmetrics SET value = {item.Value}, time =\'{item.Time}\' WHERE id = @id')";
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
                command.CommandText = $"DELETE FROM networkmetrics WHERE id={id}";
                command.ExecuteNonQuery();
            }
        }
    }

    #endregion
}