using MetricsAgent.Interfaces;
using MetricsAgent.Models;

namespace MetricsAgent.Response;

public class RAMMetricsResponse : IResponse<RamMetrics>
{
    public List<RamMetrics> ResponseDTO { get; set; }
    public RAMMetricsResponse()
    {
        ResponseDTO = new();
    }
}
