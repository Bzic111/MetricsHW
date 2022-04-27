using AutoMapper;
using Dapper;
using MetricsAgent.DAL.DTO;
using MetricsAgent.DAL.Models;
using MetricsAgent.Interfaces;
using System.Data.SQLite;

namespace MetricsAgent.DAL.Repositoryes;

public class DotNetMetricsRepository : IDotNetMetricsRepository
{
    private readonly string _connectionString;
    private readonly IMapper _mapper;
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
                    $"INSERT INTO {_table}(value, DateTime) " +
                    $"VALUES({item.Value}, \'{item.DateTime}\')");
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
                                $"FROM {_table}").ToList());
        }
    }

    public DotNetMetrics GetById(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return _mapper.Map<DotNetMetrics>(connection
                                                .QuerySingle<DotNetMetricsDTO>(
                                                    $"SELECT Id, datetime, Value " +
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
                                $"SELECT Id, datetime, Value " +
                                $"FROM {_table} " +
                                $"WHERE datetime >= '{fromStr}' " +
                                $"AND datetime <= '{toStr}'")
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

    private List<DotNetMetrics> Remap(List<DotNetMetricsDTO> list)
    {
        var result = new List<DotNetMetrics>();
        for (int i = 0; i < list.Count(); i++)
            result.Add(_mapper.Map<DotNetMetrics>(list[i]));
        return result;
    }

    #endregion
}
