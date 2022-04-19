using MetricsAgent.Models;

namespace MetricsAgent.Interfaces;

public interface IRepository<T> where T : class
{
    void Create(T item);
    void Delete(int id);
    void Update(T item);
    List<T> GetAll();
    List<T> GetByTimeFilter(DateTime from, DateTime to);
    T GetById(int id);

}

public interface ICPUMetricsRepository : IRepository<CpuMetric>
{
    CpuMetric GetAllWithPercentile(double percentile);
    CpuMetric GetByTimeFilterWithPercentile(double percentile, DateTime from, DateTime to);
    //CpuMetric GetCurrent();

}
