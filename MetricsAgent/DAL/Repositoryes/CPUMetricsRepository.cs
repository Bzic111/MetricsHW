using AutoMapper;
using Dapper;
using MetricsAgent.DTO;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Repositoryes;

public class CPUMetricsRepository : ICPUMetricsRepository
{
    public string _connectionString;
    private readonly string _table; // = "cpumetrics";
    private readonly IMapper _mapper;

    public CPUMetricsRepository(IConfiguration configuration,IMapper mapper)
    {
        _connectionString = configuration.GetConnectionString("SQLiteDB");
        _table = configuration.GetValue<string>($"Tables:{GetType().Name}");
        _mapper = mapper;
    }

    #region Create

    public void Create(CpuMetric item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection
                .Execute(
                    $"INSERT INTO {_table}(value, time) " +
                    $"VALUES({item.Value}, \'{item.DateTime}\')");
        }
    }

    #endregion

    #region Read

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

    public CpuMetric GetById(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return _mapper.Map<CpuMetric>(connection
                .QuerySingle<CpuMetricDTO>(
                $"SELECT Id, datetime, Value " +
                $"FROM {_table} WHERE id = {id}"));
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
                $"SELECT Id, datetime, Value " +
                $"FROM {_table} " +
                $"WHERE datetime >= '{fromStr}' " +
                $"AND datetime <= '{toStr}'")
                .ToList());
            //var result = connection
            //    .Query<CpuMetricDTO>(
            //    $"SELECT Id, datetime, Value " +
            //    $"FROM {_table} " +
            //    $"WHERE datetime >= '{fromStr}' " +
            //    $"AND datetime <= '{toStr}'")
            //    .ToList();

            //List<CpuMetric> answer = new();
            //for (int i = 0; i < result.Count(); i++)
            //    answer.Add(_mapper.Map<CpuMetric>(result[i]));

            //return answer;
            
        }
    }

    public CpuMetric GetAllWithPercentile(double percentile) 
        => GetPercentile(percentile, GetAll());

    public CpuMetric GetByTimeFilterWithPercentile(double percentile, DateTime from, DateTime to) 
        => GetPercentile(percentile, GetByTimeFilter(from, to));

    #endregion

    #region Update

    public void Update(CpuMetric item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Execute(
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
            connection.Execute(
                $"DELETE " +
                $"FROM {_table} " +
                $"WHERE id={id}");
        }
    }

    #endregion

    #region Private
    private List<CpuMetric> Remap(List<CpuMetricDTO> list)
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
