using MetricsAgent.DAL.Models;
using MetricsAgent.Interfaces;

namespace MetricsAgent.DAL.Response;

public class HDDMetrcsResponse : IResponse<HddMetrics>
{
    public List<HddMetrics> ResponseDTO { get; set; }
    public HDDMetrcsResponse()
    {
        ResponseDTO = new();
    }
}