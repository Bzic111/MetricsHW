using AutoMapper;
using Dapper;
using MetricsManagerHW.DAL.DTO;
using MetricsManagerHW.DAL.Models;
using MetricsManagerHW.Interface;
using System.Data.SQLite;

namespace MetricsManagerHW.DAL.Repository
{
    public class RAMMetricsRepository : IRamMetricsRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        private readonly string _table;

        public RAMMetricsRepository(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetConnectionString("SQLiteDB");
            _table = configuration.GetValue<string>($"Tables:{GetType().Name}");
            _mapper = mapper;
        }

        #region Create

        public void Create(RamMetrics item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection
                .Execute(
                    $"INSERT INTO {_table}(value, DateTime) " +
                    $"VALUES({item.Value}, \'{item.DateTime}\')");
            }
        }

        #endregion

        #region Read

        public List<RamMetrics> GetAll()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return Remap(connection
                               .Query<RamMetricsDTO>(
                                   $"SELECT * " +
                                   $"FROM {_table}").ToList());
            }
        }

        public RamMetrics GetById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return _mapper.Map<RamMetrics>(connection
                                                  .QuerySingle<RamMetricsDTO>(
                                                      $"SELECT Id, datetime, Value " +
                                                      $"FROM {_table} WHERE id = {id}"));
            }
        }

        public List<RamMetrics> GetByTimeFilter(DateTime from, DateTime to)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                string fromStr = from.ToString("s");
                string toStr = to.ToString("s");
                return Remap(connection
                                .Query<RamMetricsDTO>(
                                    $"SELECT Id, datetime, Value " +
                                    $"FROM {_table} " +
                                    $"WHERE datetime >= '{fromStr}' " +
                                    $"AND datetime <= '{toStr}'")
                                .ToList());
            }
        }

        #endregion

        #region Update

        public void Update(RamMetrics item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection
                    .Execute(
                        $"UPDATE {_table} " +
                        $"SET value = {item.Value}, datetime = \'{item.DateTime}\' " +
                        $"WHERE id = {item.Id}");
            }
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection
                    .Execute(
                        $"DELETE " +
                        $"FROM {_table} " +
                        $"WHERE id={id}");
            }
        }

        #endregion

        #region Private

        private List<RamMetrics> Remap(List<RamMetricsDTO> list)
        {
            var result = new List<RamMetrics>();
            for (int i = 0; i < list.Count(); i++)
                result.Add(_mapper.Map<RamMetrics>(list[i]));
            return result;
        }

        #endregion
    }
}