using MetricsAgent.DAL.Models;
using MetricsAgent.Interfaces;

namespace MetricsAgent.DAL.Response;

public class DotNetMetricsResponse : IResponse<DotNetMetrics>
{
    public List<DotNetMetrics> ResponseDTO { get; set; }
    public DotNetMetricsResponse()
    {
        ResponseDTO = new List<DotNetMetrics>();
    }
}
