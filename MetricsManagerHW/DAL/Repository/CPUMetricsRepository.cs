using AutoMapper;
using Dapper;
using MetricsManagerHW.DAL.DTO;
using MetricsManagerHW.DAL.Models;
using MetricsManagerHW.Interface;
using System.Data.SQLite;

namespace MetricsManagerHW.DAL.Repository;

public class CPUMetricsRepository : ICPUMetricsRepository
{
    private readonly string _connectionString;
    private readonly string _table; // = "cpumetrics";
    private readonly IMapper _mapper;// { get => _mapper; init => _mapper = value; }
    private readonly ILogger<CPUMetricsRepository> _logger;

    public CPUMetricsRepository(IConfiguration configuration, IMapper mapper, ILogger<CPUMetricsRepository> logger)
    {
        _connectionString = configuration.GetConnectionString("SQLiteDB");
        _table = configuration.GetValue<string>($"Tables:{GetType().Name}");
        _mapper = mapper;
        _logger = logger;
    }

    #region Create

    public void Create(CpuMetric item)
    {
        _logger.LogInformation($"CPU repository Create item {item.DateTime}\n{item.Value}\n{item.AgentId}\n{item}");
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

    public CpuMetric GetById(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return _mapper.Map<CpuMetric>(connection
                .QuerySingle<CpuMetricDTO>(
                $"SELECT * " +
                $"FROM {_table} WHERE id = {id}"));
        }
    }
    public CpuMetric GetByAgentId(int agentId)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return _mapper.Map<CpuMetric>(connection
                .QuerySingle<CpuMetricDTO>(
                $"SELECT * " +
                $"FROM {_table} WHERE agentId = {agentId}"));
        }
    }
    public List<CpuMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return Remap(connection
                            .Query<CpuMetricDTO>(
                                $"SELECT * " +
                                $"FROM {_table}").ToList());
        }
    }
    public List<CpuMetric> GetAllOfAgent(int agentId)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return Remap(connection
                            .Query<CpuMetricDTO>(
                                $"SELECT * " +
                                $"FROM {_table}" +
                                $"WHERE agentId = {agentId}")
                            .ToList());
        }
    }
    public List<CpuMetric> GetByTimeFilter(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            string fromStr = from.ToString("s");
            string toStr = to.ToString("s");
            return Remap(connection
                .Query<CpuMetricDTO>(
                $"SELECT * " +
                $"FROM {_table} " +
                $"WHERE datetime >= '{fromStr}' " +
                $"AND datetime <= '{toStr}'")
                .ToList());
        }
    }
    public List<CpuMetric> GetByAgentIdWithTimeFilter(DateTime from, DateTime to, int agentId)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            string fromStr = from.ToString("s");
            string toStr = to.ToString("s");
            return Remap(connection
                .Query<CpuMetricDTO>(
                $"SELECT * " +
                $"FROM {_table} " +
                $"WHERE datetime >= '{fromStr}' " +
                $"AND datetime <= '{toStr}'" +
                $"AND agentId = {agentId}")
                .ToList());
        }
    }
    public CpuMetric GetAllWithPercentile(double percentile)
        => GetPercentile(percentile, GetAll());
    public CpuMetric GetByTimeFilterWithPercentile(double percentile, DateTime from, DateTime to)
        => GetPercentile(percentile, GetByTimeFilter(from, to));
    public CpuMetric GetAllOfAgentWithPercentile(double percentile, int agentId)
        => GetPercentile(percentile, GetAllOfAgent(agentId));
    public CpuMetric GetByAgentIdWithTimeFilterWithPercentile(double percentile, DateTime from, DateTime to, int agentId)
        => GetPercentile(percentile, GetByAgentIdWithTimeFilter(from, to, agentId));

    #endregion

    #region Update

    public void Update(CpuMetric item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Execute(
                $"UPDATE {_table} " +
                $"SET agentId = {item.AgentId}, " +
                $"value = {item.Value}, " +
                $"datetime = \'{item.DateTime}\' " +
                $"WHERE id = {item.Id}");
        }
    }

    #endregion

    #region Delete

    public void Delete(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Execute(
                $"DELETE " +
                $"FROM {_table} " +
                $"WHERE id={id}");
        }
    }

    #endregion

    #region Private

    private List<CpuMetric> Remap(List<CpuMetricDTO> list)/* => IRepository<CpuMetric>.Remap(list, _mapper);*/
    {
        var result = new List<CpuMetric>();
        for (int i = 0; i < list.Count(); i++)
            result.Add(_mapper.Map<CpuMetric>(list[i]));
        return result;
    }

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
