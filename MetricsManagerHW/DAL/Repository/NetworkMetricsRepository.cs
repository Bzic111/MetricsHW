using AutoMapper;
using Dapper;
using MetricsManagerHW.DAL.DTO;
using MetricsManagerHW.DAL.Models;
using MetricsManagerHW.Interface;
using System.Data.SQLite;

namespace MetricsManagerHW.DAL.Repository;

public class NetworkMetricsRepository : INetworkMetricsRepository
{
    private readonly string _connectionString;
    public IMapper _mapper { get; init; }
    private readonly string _table;

    public NetworkMetricsRepository(IConfiguration configuration, IMapper mapper)
    {
        _connectionString = configuration.GetConnectionString("SQLiteDB");
        _table = configuration.GetValue<string>($"Tables:{GetType().Name}");
        _mapper = mapper;
    }

    #region Create

    public void Create(NetworkMetrics item)
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

    public List<NetworkMetrics> GetAll()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return Remap(connection
                               .Query<NetworkMetricsDTO>(
                                   $"SELECT * " +
                                   $"FROM {_table}")
                               .ToList());
        }
    }

    public NetworkMetrics GetById(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return _mapper.Map<NetworkMetrics>(connection
                                                  .QuerySingle<NetworkMetricsDTO>(
                                                      $"SELECT * " +
                                                      $"FROM {_table} WHERE id = {id}"));
        }
    }

    public List<NetworkMetrics> GetByTimeFilter(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            string fromStr = from.ToString("s");
            string toStr = to.ToString("s");
            return Remap(connection
                            .Query<NetworkMetricsDTO>(
                                $"SELECT * " +
                                $"FROM {_table} " +
                                $"WHERE datetime >= '{fromStr}' " +
                                $"AND datetime <= '{toStr}'")
                            .ToList());
        }
    }

    public NetworkMetrics GetByAgentId(int agentId)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return _mapper.Map<NetworkMetrics>(connection
                                                .QuerySingle<NetworkMetricsDTO>(
                                                    $"SELECT * " +
                                                    $"FROM {_table} " +
                                                    $"WHERE agentId = {agentId}"));
        }
    }

    public List<NetworkMetrics> GetAllOfAgent(int agentId)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return Remap(connection
                            .Query<NetworkMetricsDTO>(
                                $"SELECT * " +
                                $"FROM {_table} " +
                                $"WHERE agentId = {agentId}")
                            .ToList());
        }
    }

    public List<NetworkMetrics> GetByAgentIdWithTimeFilter(DateTime from, DateTime to, int agentId)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            string fromStr = from.ToString("s");
            string toStr = to.ToString("s");
            return Remap(connection
                            .Query<NetworkMetricsDTO>(
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

    public void Update(NetworkMetrics item)
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

    private List<NetworkMetrics> Remap(List<NetworkMetricsDTO> list) => IRepository<NetworkMetrics>.Remap(list, _mapper);
    //{
    //    var result = new List<NetworkMetrics>();
    //    for (int i = 0; i < list.Count(); i++)
    //        result.Add(_mapper.Map<NetworkMetrics>(list[i]));
    //    return result;
    //}

    #endregion
}
