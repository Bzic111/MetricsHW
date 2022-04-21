using MetricsAgent.Interfaces;
using MetricsAgent.Models;

namespace MetricsAgent.Response;

public class DotNetMetricsResponse : IResponse<DotNetMetrics>
{
    public List<DotNetMetrics> ResponseDTO { get; set; }
    public DotNetMetricsResponse()
    {
        ResponseDTO = new List<DotNetMetrics>();
    }
}
