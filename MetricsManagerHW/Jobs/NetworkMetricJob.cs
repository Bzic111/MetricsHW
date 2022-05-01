using MetricsManagerHW.DAL.Client;
using MetricsManagerHW.DAL.Models;
using MetricsManagerHW.DAL.Repository;
using MetricsManagerHW.Interface;
using Quartz;

namespace MetricsManagerHW.Jobs;
public class NetworkMetricJob : IJob
{
    private INetworkMetricsRepository _repository;
    private MetricsAgentClient _httpClientMetricsAgent;
    private AgentsRepository _agentsRepository;
    public NetworkMetricJob(INetworkMetricsRepository repository,
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
        List<ResponseFromAgent<NetworkMetrics>> responseList = new();

        foreach (var agent in agents)
        {
            RequestToAgent request = new RequestToAgent()
            {
                ApiRoute = @"metrics/network",
                From = DateTime.Now.AddMinutes(-1),
                To = DateTime.Now,
                Agent = new()
                {
                    AgentId = agent.Id,
                    AgentAdress = agent.Adress! /*@"http://localhost:5056"*/
                }
            };
            responseList.Add(_httpClientMetricsAgent.GetMetricsFromAgent<NetworkMetrics>(request));
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