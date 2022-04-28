using MetricsManagerHW.Interface;

namespace MetricsManagerHW.DAL.Request;

public abstract class MetricsAgentRequest : IRequest
{
    public DateTime Date { get; set; }
    public DateTime SecondDate { get; set; }
    public int Value { get; set; }
}
