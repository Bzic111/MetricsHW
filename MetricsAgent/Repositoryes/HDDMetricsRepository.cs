using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Repositoryes;

public class HDDMetricsRepository : IRepository<HddMetrics>
{
    private readonly string _connectionString;
    public HDDMetricsRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SQLiteDB");
    }
    // hddmetrics
    #region Create

    public void Create(HddMetrics item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"INSERT INTO hddmetrics(value, datetime)VALUES({item.Value},\'{item.Time}\')";
                command.ExecuteNonQuery();
            }
        }
    }

    #endregion

    #region Read

    public IList<HddMetrics> GetAll()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "SELECT * FROM hddmetrics;";
                var result = new List<HddMetrics>();
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

    public HddMetrics GetById(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"SELECT id,value,datetime FROM hddmetrics WHERE id = {id}";
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

    public IList<HddMetrics> GetByTimeFilter(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"SELECT * FROM hddmetrics WHERE datetime BETWEEN \"{from.ToString("s")}\" AND \"{to.ToString("s")}\";";
                var result = new List<HddMetrics>();
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

    public void Update(HddMetrics item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"UPDATE hddmetrics SET value = {item.Value}, time =\'{item.Time}\' WHERE id = @id')";
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
                command.CommandText = $"DELETE FROM hddmetrics WHERE id={id}";
                command.ExecuteNonQuery();
            }
        }
    }

    #endregion
}