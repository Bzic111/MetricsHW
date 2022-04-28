using AutoMapper;

namespace MetricsManagerHW.Interface;

public interface IRepository<T> where T : class
{
    #region Create

    /// <summary>
    /// Создать метрику в БД
    /// </summary>
    /// <param name="item">Метрика</param>
    void Create(T item);

    #endregion

    #region Read

    /// <summary>
    /// Получить конкретную метрику
    /// </summary>
    /// <param name="id">Идентификатор метрики</param>
    /// <returns>Метрика</returns>
    T GetById(int id);

    /// <summary>
    /// Получить метрику конкретного агента
    /// </summary>
    /// <param name="agentId">Идентификатор агента</param>
    /// <returns>Метрика</returns>
    T GetByAgentId(int agentId);
    
    /// <summary>
    /// Получить все метрики
    /// </summary>
    /// <returns>Список метрик</returns>
    List<T> GetAll();

    /// <summary>
    /// Получить все метрики с конкретного агента
    /// </summary>
    /// <param name="agentId">Идентификатор агента</param>
    /// <returns>Список метрик</returns>
    List<T> GetAllOfAgent(int agentId);

    /// <summary>
    /// Получить все метрики в промежутке времени
    /// </summary>
    /// <param name="from">Начало промежутка</param>
    /// <param name="to">конец промежутка</param>
    /// <returns>Список метрик</returns>
    List<T> GetByTimeFilter(DateTime from, DateTime to);

    /// <summary>
    /// Получить все метрики с конкретного агента в промежутке времени
    /// </summary>
    /// <param name="from">Начало промежутка</param>
    /// <param name="to">конец промежутка</param>
    /// <param name="agentId">Идентификатор агента</param>
    /// <returns>Список метрик</returns>
    List<T> GetByAgentIdWithTimeFilter(DateTime from, DateTime to, int agentId);

    #endregion

    #region Update

    /// <summary>
    /// Изменить метрику в БД
    /// </summary>
    /// <param name="item">Метрика</param>
    void Update(T item);

    #endregion

    #region Delete

    /// <summary>
    /// Удалить метрику в БД
    /// </summary>
    /// <param name="id">Идентификатор метрики</param>
    void Delete(int id);

    #endregion

    #region Private
    static List<T> Remap<T2>(List<T2> list, IMapper _mapper) 
    {
        var result = new List<T>();
        for (int i = 0; i < list.Count(); i++)
            result.Add(_mapper.Map<T>(list[i]));
        return result;
    }
    #endregion
}
