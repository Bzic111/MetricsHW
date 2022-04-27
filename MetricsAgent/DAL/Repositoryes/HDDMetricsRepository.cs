using Dapper;
using AutoMapper;
using MetricsAgent.Interfaces;
using System.Data.SQLite;
using MetricsAgent.DAL.DTO;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repositoryes
{
    public class HDDMetricsRepository : IHddMetricsRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        private readonly string _table;
        public HDDMetricsRepository(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetConnectionString("SQLiteDB");
            _table = configuration.GetValue<string>($"Tables:{GetType().Name}");
            _mapper = mapper;
        }
        // hddmetrics
        #region Create

        public void Create(HddMetrics item)
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

        public List<HddMetrics> GetAll()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return Remap(connection
                            .Query<HddMetricsDTO>(
                                $"SELECT * " +
                                $"FROM {_table}").ToList());
            }
        }

        public HddMetrics GetById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return _mapper.Map<HddMetrics>(connection
                                                  .QuerySingle<HddMetricsDTO>(
                                                      $"SELECT Id, datetime, Value " +
                                                      $"FROM {_table} WHERE id = {id}"));
            }
        }

        public List<HddMetrics> GetByTimeFilter(DateTime from, DateTime to)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                string fromStr = from.ToString("s");
                string toStr = to.ToString("s");
                return Remap(connection
                                .Query<HddMetricsDTO>(
                                    $"SELECT Id, datetime, Value " +
                                    $"FROM {_table} " +
                                    $"WHERE datetime >= '{fromStr}' " +
                                    $"AND datetime <= '{toStr}'")
                                .ToList());
            }
        }

        #endregion

        #region Update

        public void Update(HddMetrics item)
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

        private List<HddMetrics> Remap(List<HddMetricsDTO> list)
        {
            var result = new List<HddMetrics>();
            for (int i = 0; i < list.Count(); i++)
                result.Add(_mapper.Map<HddMetrics>(list[i]));
            return result;
        }

        #endregion
    }
}