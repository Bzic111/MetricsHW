using MetricsManagerHW.Interface;
using MetricsManagerHW.DAL.Models;
using Quartz;
using MetricsManagerHW.DAL.Client;
using MetricsManagerHW.DAL.Repository;

namespace MetricsManagerHW.Jobs;

public class DotNetMetricJob : IJob
{
    private IDotNetMetricsRepository _repository;
    private MetricsAgentClient _httpClientMetricsAgent;
    private AgentsRepository _agentsRepository;
    public DotNetMetricJob(IDotNetMetricsRepository repository,
                        AgentsRepository agentsRepository,
                        MetricsAgentClient client)
    {
        _repository = repository;
        _httpClientMetricsAgent = client;
        _agentsRepository = agentsRepository;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var agents = _agentsRepository.GetAll(true);
        List<ResponseFromAgent<DotNetMetrics>> responseList = new();

        foreach (var agent in agents)
        {
            RequestToAgent request = new RequestToAgent()
            {
                ApiRoute = @"metrics/dotnet/errors-count",
                From = DateTime.Now.AddMinutes(-1),
                To = DateTime.Now,
                Agent = new()
                {
                    AgentId = agent.Id,
                    AgentAdress = agent.Adress! /*@"http://localhost:5056"*/
                }
            };
            responseList.Add(_httpClientMetricsAgent.GetMetricsFromAgent<DotNetMetrics>(request));
        }

        foreach (var response in responseList)
        {
            if (response is not null)
            {
                SendDataToDB(response.Collection, _repository);
            }
        }
        return Task.CompletedTask;
        void SendDataToDB<T>(IEnumerable<T> list, IRepository<T> repository) where T : class
        {
            foreach (var item in list)
            {
                repository.Create(item);
            }
        }
    }
}
