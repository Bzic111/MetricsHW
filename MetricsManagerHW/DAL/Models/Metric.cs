namespace MetricsManagerHW.DAL.Models
{
    public interface IMetric
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int Value { get; set; }
        public DateTime DateTime { get; set; }
    }
    public abstract class Metric : IMetric
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int Value { get; set; }
        public DateTime DateTime { get; set; }
    }
}