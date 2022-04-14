namespace MetricsAgent.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        void Delete(int id);
        void Update(T item);
        IList<T> GetAll();
        IList<T> GetByTimeFilter(DateTime from, DateTime to);
        T GetById(int id);

        //T GetAllWithPercentile(double percentile);
        //T GetByTimeFilterWithPercentile(double percentile, DateTime from, DateTime to);
        //T GetCurrent();
    }
}