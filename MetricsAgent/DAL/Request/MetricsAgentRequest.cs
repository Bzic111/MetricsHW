using MetricsAgent.Interfaces;

namespace MetricsAgent.Request;

public abstract class MetricsAgentRequest : IRequest
{
    public DateTime Date { get; set; }
    public int Value { get; set; }
}
