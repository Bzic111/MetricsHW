using MetricsAgent.DAL.Models;
using MetricsAgent.Interfaces;

namespace MetricsAgent.DAL.Response;

public class NetworkMetrcsResponse : IResponse<NetworkMetrics>
{
    public List<NetworkMetrics> ResponseDTO { get; set; }
    public NetworkMetrcsResponse()
    {
        ResponseDTO = new();
    }
}
