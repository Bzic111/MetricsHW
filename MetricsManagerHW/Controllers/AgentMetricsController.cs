using Dapper;
using MetricsManagerHW.DAL;
using MetricsManagerHW.DAL.Client;
using MetricsManagerHW.DAL.Models;
using MetricsManagerHW.DAL.Repository;
using MetricsManagerHW.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerHW.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgentMetricsController : ControllerBase
{
    private MetricsAgentClient _httpClientMetricsAgent;
    private AgentsRepository _agentsRepository;
    private ICPUMetricsRepository _repository;
    public AgentMetricsController(ICPUMetricsRepository repository,
                    AgentsRepository agentsRepository,
                    MetricsAgentClient client)
    {
        _httpClientMetricsAgent = client;
        _agentsRepository = agentsRepository;
        _repository = repository;
    }
    [HttpGet("fromAgent")]
    public IActionResult Meth()
    {
        var agents = _agentsRepository.GetAll(true);
        List<ResponseFromAgent<CpuMetric>> responseList = new();

        foreach (var agent in agents)
        {
            RequestToAgent request = new RequestToAgent()
            {
                ApiRoute = @"metrics/cpu",
                From = DateTime.Now.AddMinutes(-1),
                To = DateTime.Now,
                Agent = new()
                {
                    AgentId = agent.Id,
                    AgentAdress = agent.Adress! /*@"http://localhost:5056"*/
                }
            };
            responseList.Add(_httpClientMetricsAgent.GetMetricsFromAgent<CpuMetric>(request));
        }
        foreach (var response in responseList)
        {
            SendDataToDB(response.Collection, _repository);
        }
        return Ok(responseList[0].Collection);
    }
    private void SendDataToDB<T>(IEnumerable<T> list, IRepository<T> repository) where T : class
    {
        foreach (var item in list)
        {
            repository.Create(item);
        }
    }
}
