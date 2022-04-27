using MetricsManagerHW.DAL.Models;
using MetricsManagerHW.Interface;

namespace MetricsManagerHW.DAL.Response;

public class CPUMetricsResponse : IResponse<CpuMetric>
{
    public List<CpuMetric> ResponseDTO { get; set; }
    public CPUMetricsResponse()
    {
        ResponseDTO = new List<CpuMetric>();
    }
}
