using MetricsAgent.Interfaces;
using MetricsAgent.Models;

namespace MetricsAgent.Response;

public class NetworkMetrcsResponse : IResponse<NetworkMetrics>
{
    public List<NetworkMetrics> ResponseDTO { get; set; }
    public NetworkMetrcsResponse()
    {
        ResponseDTO = new();
    }
}
