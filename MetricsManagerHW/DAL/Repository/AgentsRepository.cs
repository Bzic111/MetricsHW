using AutoMapper;
using Dapper;
using MetricsManagerHW.DAL.DTO;
using MetricsManagerHW.DAL.Models;
using MetricsManagerHW.Interface;
using System.Data.SQLite;

namespace MetricsManagerHW.DAL.Repository;

public class AgentsRepository
{
    public string _connectionString;
    private readonly string _table; // = "agents";
    private readonly IMapper _mapper;

    public AgentsRepository(IConfiguration configuration, IMapper mapper)
    {
        _connectionString = configuration.GetConnectionString("SQLiteDB");
        _table = configuration.GetValue<string>($"Tables:{GetType().Name}");
        _mapper = mapper;
    }

    #region Create

    public void Create(Agent item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection
                .Execute(
                    $"INSERT INTO {_table}(Adress, Enabled) " +
                    $"VALUES(\'{item.Adress}\', {item.Enabled})");
        }
    }

    #endregion

    #region Read

    public List<Agent> GetAll()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return Remap(connection
                            .Query<AgentDTO>(
                                $"SELECT * " +
                                $"FROM {_table}")
                            .ToList());
        }
    }

    public Agent GetById(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return _mapper.Map<Agent>(connection
                .QuerySingle<AgentDTO>(
                $"SELECT Id, datetime, Value " +
                $"FROM {_table} WHERE id = {id}"));
        }
    }

    public List<Agent> GetAll(bool enabled)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            string query = $"SELECT * FROM {_table} WHERE enabled = {(enabled ? 1 : 0)}";
            return Remap(connection
                            .Query<AgentDTO>(query)
                            .ToList());
        }
    }

    #endregion

    #region Update

    public void Update(Agent item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Execute(
                $"UPDATE {_table} " +
                $"SET adress = \'{item.Adress}\', enabled = {item.Enabled} " +
                $"WHERE id = {item.Id}");
        }
    }
    public void UpdateAdress(Agent item)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Execute(
                $"UPDATE {_table} " +
                $"SET adress = \'{item.Adress}\' " +
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

    private List<Agent> Remap(List<AgentDTO> list)
    {
        var result = new List<Agent>();
        for (int i = 0; i < list.Count(); i++)
            result.Add(_mapper.Map<Agent>(list[i]));
        return result;
    }

    #endregion
}
