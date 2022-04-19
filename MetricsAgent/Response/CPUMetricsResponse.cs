using MetricsAgent.Interfaces;
using MetricsAgent.Models;

namespace MetricsAgent.Response;

public class CPUMetricsResponse : IResponse<CpuMetric>
{
    public List<CpuMetric> ResponseDTO { get; set; }
    public CPUMetricsResponse()
    {
        ResponseDTO = new List<CpuMetric>();
    }
}
