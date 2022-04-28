using MetricsManagerHW.DAL.Models;

namespace MetricsManagerHW.Interface;

public interface ICPUMetricsRepository : IRepository<CpuMetric>
{
    public CpuMetric GetAllWithPercentile(double percentile);
    public CpuMetric GetByTimeFilterWithPercentile(double percentile, DateTime from, DateTime to);
    public CpuMetric GetAllOfAgentWithPercentile(double percentile, int agentId);
    public CpuMetric GetByAgentIdWithTimeFilterWithPercentile(double percentile, DateTime from, DateTime to, int agentId);

}