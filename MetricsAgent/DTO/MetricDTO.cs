namespace MetricsAgent.DTO;

public abstract class MetricDTO
{
    public int Id { get; set; }
    public int Value { get; set; }
    public DateTime Time { get; set; }
}
