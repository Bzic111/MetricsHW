using MetricsManagerHW.DAL.Models;
using MetricsManagerHW.Interface;

namespace MetricsManagerHW.DAL.Response;

public class NetworkMetrcsResponse : IResponse<NetworkMetrics>
{
    public List<NetworkMetrics> ResponseDTO { get; set; }
    public NetworkMetrcsResponse()
    {
        ResponseDTO = new();
    }
}
