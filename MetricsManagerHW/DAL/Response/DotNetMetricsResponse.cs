using MetricsManagerHW.DAL.Models;
using MetricsManagerHW.Interface;

namespace MetricsManagerHW.DAL.Response;

public class DotNetMetricsResponse : IResponse<DotNetMetrics>
{
    public List<DotNetMetrics> ResponseDTO { get; set; }
    public DotNetMetricsResponse()
    {
        ResponseDTO = new List<DotNetMetrics>();
    }
}
