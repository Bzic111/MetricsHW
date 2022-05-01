using MetricsManagerHW.DAL.Models;

namespace MetricsManagerHW.DAL.Client;

public class ResponseFromAgent<T> where T : class,IMetric
{
    public List<T> Collection { get; set; }
    public int AgentId { get; set; }
}
