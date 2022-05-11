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
