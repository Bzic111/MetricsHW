namespace MetricsManagerHW.Interface
{
    public interface IRequest
    {
        public DateTime Date { get; set; }
        public int Value { get; set; }
    }
}