using MetricsAgent.DAL.Models;
using MetricsAgent.Interfaces;

namespace MetricsAgent.DAL.Response;

public class RAMMetricsResponse : IResponse<RamMetrics>
{
    public List<RamMetrics> ResponseDTO { get; set; }
    public RAMMetricsResponse()
    {
        ResponseDTO = new();
    }
}
