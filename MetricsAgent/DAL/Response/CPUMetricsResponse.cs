using MetricsAgent.DAL.Models;
using MetricsAgent.Interfaces;

namespace MetricsAgent.DAL.Response;

public class CPUMetricsResponse : IResponse<CpuMetric>
{
    public List<CpuMetric> ResponseDTO { get; set; }
    public CPUMetricsResponse()
    {
        ResponseDTO = new List<CpuMetric>();
    }
}
