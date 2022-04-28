using Dapper;
using AutoMapper;
using MetricsManagerHW.Interface;
using System.Data.SQLite;
using MetricsManagerHW.DAL.DTO;
using MetricsManagerHW.DAL.Models;

namespace MetricsManagerHW.DAL.Repository;

public class HDDMetricsRepository : IHddMetricsRepository
{
    private readonly string _connectionString;
    public IMapper _mapper { get; init; }
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
                $"INSERT INTO {_table}(agentId, value, DateTime) " +
                $"VALUES({item.AgentId}, {item.Value}, \'{item.DateTime}\')");
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
                            $"FROM {_table}")
                        .ToList());
        }
    }

    public HddMetrics GetById(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return _mapper.Map<HddMetrics>(connection
                                              .QuerySingle<HddMetricsDTO>(
                                                  $"SELECT * " +
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
                                $"SELECT * " +
                                $"FROM {_table} " +
                                $"WHERE datetime >= '{fromStr}' " +
                                $"AND datetime <= '{toStr}'")
                            .ToList());
        }
    }
    
    public HddMetrics GetByAgentId(int agentId)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return _mapper.Map<HddMetrics>(connection
                                                .QuerySingle<HddMetricsDTO>(
                                                    $"SELECT * " +
                                                    $"FROM {_table} " +
                                                    $"WHERE agentId = {agentId}"));
        }
    }

    public List<HddMetrics> GetAllOfAgent(int agentId)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return Remap(connection
                            .Query<HddMetricsDTO>(
                                $"SELECT * " +
                                $"FROM {_table} " +
                                $"WHERE agentId = {agentId}")
                            .ToList());
        }
    }

    public List<HddMetrics> GetByAgentIdWithTimeFilter(DateTime from, DateTime to, int agentId)
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
                                $"AND datetime <= '{toStr}' " +
                                $"AND agentId = {agentId}")
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
                    $"SET agentId = {item.AgentId},value = {item.Value}, datetime = \'{item.DateTime}\' " +
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

    private List<HddMetrics> Remap(List<HddMetricsDTO> list) => IRepository<HddMetrics>.Remap(list, _mapper);
    //{
    //    var result = new List<HddMetrics>();
    //    for (int i = 0; i < list.Count(); i++)
    //        result.Add(_mapper.Map<HddMetrics>(list[i]));
    //    return result;
    //}

    #endregion
}
