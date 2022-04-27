namespace MetricsAgent.DAL.Models
{
    public abstract class Metric
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime DateTime { get; set; }
    }
}