using MetricsManagerHW.DAL.Models;
using MetricsManagerHW.Interface;

namespace MetricsManagerHW.DAL.Response;

public class HDDMetrcsResponse : IResponse<HddMetrics>
{
    public List<HddMetrics> ResponseDTO { get; set; }
    public HDDMetrcsResponse()
    {
        ResponseDTO = new();
    }
}