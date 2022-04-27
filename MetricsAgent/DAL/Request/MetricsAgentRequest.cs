using MetricsAgent.Interfaces;

namespace MetricsAgent.DAL.Request;

public abstract class MetricsAgentRequest : IRequest
{
    public DateTime Date { get; set; }
    public int Value { get; set; }
}
