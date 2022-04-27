using MetricsManagerHW.DAL.Models;

namespace MetricsManagerHW.Interface;

public interface ICPUMetricsRepository : IRepository<CpuMetric>
{
    CpuMetric GetAllWithPercentile(double percentile);
    CpuMetric GetByTimeFilterWithPercentile(double percentile, DateTime from, DateTime to);
    //CpuMetric GetCurrent();

}
public interface IAgentsRepository : IRepository<Agent>
{

}