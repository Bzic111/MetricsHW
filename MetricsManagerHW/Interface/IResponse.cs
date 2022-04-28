namespace MetricsManagerHW.Interface
{
    public interface IResponse<T> where T : class
    {
        List<T> ResponseDTO { get; set; }
    }
}