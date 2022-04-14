using MetricsAgent.DTO;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Repositoryes
{
    public class RAMMetricsRepository : IRepository<RamMetrics>
    {
        private readonly string _connectionString;

        public RAMMetricsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLiteDB");
        }

        #region Create

        public void Create(RamMetrics item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $"INSERT INTO rammetrics(value, datetime)VALUES({item.Value},\'{item.Time}\')";
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Read

        public IList<RamMetrics> GetAll()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT * FROM rammetrics;";
                    var result = new List<RamMetrics>();
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

        public RamMetrics GetById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $"SELECT id,value,datetime FROM rammetrics WHERE id = {id}";
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

        public IList<RamMetrics> GetByTimeFilter(DateTime from, DateTime to)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $"SELECT * FROM cpumetrics WHERE datetime BETWEEN \"{from.ToString("s")}\" AND \"{to.ToString("s")}\";";
                    var result = new List<RamMetrics>();
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

        public void Update(RamMetrics item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $"UPDATE rammetrics SET value = {item.Value}, time =\'{item.Time}\' WHERE id = @id')";
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
                    command.CommandText = $"DELETE FROM rammetrics WHERE id={id}";
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion
    }
}