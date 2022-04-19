using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Repositoryes;

public class CPUMetricsRepository : ICPUMetricsRepository
{
    public string _connectionString;

    public CPUMetricsRepository()
    {
        _connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    }
    //public CPUMetricsRepository(IConfiguration configuration)
    //{
    //    _connectionString = configuration.GetConnectionString("SQLiteDB");
    //}

    #region Create

    public void Create(CpuMetric item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"INSERT INTO cpumetrics(value, datetime)VALUES({item.Value},\'{item.Time}\')";
                command.ExecuteNonQuery();
            }
        }
    }

    #endregion

    #region Read

    public List<CpuMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "SELECT * FROM cpumetrics;";
                var result = new List<CpuMetric>();
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

    public CpuMetric GetById(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"SELECT id,value,datetime FROM cpumetrics WHERE id = {id}";
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

    public List<CpuMetric> GetByTimeFilter(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"SELECT * FROM cpumetrics WHERE datetime BETWEEN \"{from.ToString("s")}\" AND \"{to.ToString("s")}\";";
                var result = new List<CpuMetric>();
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

    public CpuMetric GetAllWithPercentile(double percentile)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "SELECT * FROM cpumetrics;";
                var result = new List<CpuMetric>();
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
                    return GetPercentile(percentile, result);
                }
            }
        }
    }

    public CpuMetric GetByTimeFilterWithPercentile(double percentile, DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"SELECT * FROM cpumetrics WHERE datetime BETWEEN \"{from.ToString("s")}\" AND \"{to.ToString("s")}\";";
                var result = new List<CpuMetric>();
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
                    return GetPercentile(percentile, result);
                }
            }
        }
    }

    #endregion

    #region Update

    public void Update(CpuMetric item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"UPDATE cpumetrics SET value = {item.Value}, time =\'{item.Time}\' WHERE id = @id')";
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
                command.CommandText = $"DELETE FROM cpumetrics WHERE id={id}";
                command.ExecuteNonQuery();
            }
        }
    }

    #endregion

    #region PrivateMethod
    private CpuMetric GetPercentile(double percentile, List<CpuMetric> list)
    {
        List<int> temp = new();

        foreach (var item in list)
            temp.Add(item.Value);

        temp.Sort();
        var value = temp[(int)(percentile / 100 * list.Count)];

        foreach (var item in list)
            if (item.Value >= value)
                return item;

        return null!;
    }

    #endregion
}
