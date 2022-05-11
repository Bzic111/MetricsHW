namespace MetricsAgent.DAL.DTO;

public abstract class MetricDTO
{
    public int Id { get; set; }
    public int Value { get; set; }
    public string DateTime { get; set; } // DateTime
}
