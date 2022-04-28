namespace MetricsAgent.Interfaces
{
    public interface IRequest
    {
        public DateTime Date { get; set; }
        public int Value { get; set; }
    }
}