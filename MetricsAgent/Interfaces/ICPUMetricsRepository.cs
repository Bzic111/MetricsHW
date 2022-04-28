using MetricsAgent.DAL.Models;

namespace MetricsAgent.Interfaces;

public interface ICPUMetricsRepository : IRepository<CpuMetric>
{
    CpuMetric GetAllWithPercentile(double percentile);
    CpuMetric GetByTimeFilterWithPercentile(double percentile, DateTime from, DateTime to);
    //CpuMetric GetCurrent();

}
