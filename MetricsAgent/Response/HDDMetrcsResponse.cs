using MetricsAgent.Interfaces;
using MetricsAgent.Models;

namespace MetricsAgent.Response;

public class HDDMetrcsResponse : IResponse<HddMetrics>
{
    public List<HddMetrics> ResponseDTO { get; set; }
    public HDDMetrcsResponse()
    {
        ResponseDTO = new();
    }
}