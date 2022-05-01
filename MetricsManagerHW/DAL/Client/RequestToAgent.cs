using MetricsManagerHW.ext;

namespace MetricsManagerHW.DAL.Client;

public class RequestToAgent
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public AgentInfo Agent { get; set; }
    public string ApiRoute { get; set; }
}
