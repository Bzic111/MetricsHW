using AutoMapper;
using Dapper;
using MetricsManagerHW.DAL.DTO;
using MetricsManagerHW.DAL.Models;
using MetricsManagerHW.Interface;
using System.Data.SQLite;

namespace MetricsManagerHW.DAL.Repository;

public class DotNetMetricsRepository : IDotNetMetricsRepository
{
    private readonly IMapper _mapper;// { get => _mapper; init => _mapper = value; }
    private readonly string _connectionString;
    private readonly string _table;

    public DotNetMetricsRepository(IConfiguration configuration, IMapper mapper)
    {
        _connectionString = configuration.GetConnectionString("SQLiteDB");
        _table = configuration.GetValue<string>($"Tables:{GetType().Name}");
        _mapper = mapper;
    }

    #region Create

    public void Create(DotNetMetrics item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection
                .Execute(
                    $"INSERT INTO {_table}(agentId, value, DateTime) " +
                    $"VALUES({item.AgentId}, {item.Value}, \'{item.DateTime:s}\')");
        }
    }

    #endregion

    #region Read

    // dotnetmetrics
    public List<DotNetMetrics> GetAll()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return Remap(connection
                            .Query<DotNetMetricsDTO>(
                                $"SELECT * " +
                                $"FROM {_table}")
                            .ToList());
        }
    }

    public DotNetMetrics GetById(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return _mapper.Map<DotNetMetrics>(connection
                                                .QuerySingle<DotNetMetricsDTO>(
                                                    $"SELECT * " +
                                                    $"FROM {_table} WHERE id = {id}"));
        }
    }

    public List<DotNetMetrics> GetByTimeFilter(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            string fromStr = from.ToString("s");
            string toStr = to.ToString("s");
            return Remap(connection
                            .Query<DotNetMetricsDTO>(
                                $"SELECT * " +
                                $"FROM {_table} " +
                                $"WHERE datetime >= '{fromStr}' " +
                                $"AND datetime <= '{toStr}'")
                            .ToList());
        }
    }

    public DotNetMetrics GetByAgentId(int agentId)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return _mapper.Map<DotNetMetrics>(connection
                                                .QuerySingle<DotNetMetricsDTO>(
                                                    $"SELECT * " +
                                                    $"FROM {_table} " +
                                                    $"WHERE agentId = {agentId}"));
        }
    }

    public List<DotNetMetrics> GetAllOfAgent(int agentId)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return Remap(connection
                            .Query<DotNetMetricsDTO>(
                                $"SELECT * " +
                                $"FROM {_table} " +
                                $"WHERE agentId = {agentId}")
                            .ToList());
        }
    }

    public List<DotNetMetrics> GetByAgentIdWithTimeFilter(DateTime from, DateTime to, int agentId)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            string fromStr = from.ToString("s");
            string toStr = to.ToString("s");
            return Remap(connection
                            .Query<DotNetMetricsDTO>(
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

    public void Update(DotNetMetrics item)
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

    private List<DotNetMetrics> Remap(List<DotNetMetricsDTO> list)/* => IRepository<DotNetMetrics>.Remap(list, _mapper);*/
    {
        var result = new List<DotNetMetrics>();
        for (int i = 0; i < list.Count(); i++)
            result.Add(_mapper.Map<DotNetMetrics>(list[i]));
        return result;
    }

    #endregion
}
