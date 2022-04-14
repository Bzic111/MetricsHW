namespace MetricsAgent.Models;

public abstract class Metric
{
    public int Id { get; set; }
    public int Value { get; set; }
    public DateTime Time { get; set; }
}
