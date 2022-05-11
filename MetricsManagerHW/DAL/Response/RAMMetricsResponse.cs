using MetricsManagerHW.DAL.Models;
using MetricsManagerHW.Interface;

namespace MetricsManagerHW.DAL.Response;

public class RAMMetricsResponse : IResponse<RamMetrics>
{
    public List<RamMetrics> ResponseDTO { get; set; }
    public RAMMetricsResponse()
    {
        ResponseDTO = new();
    }
}
