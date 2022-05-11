using System;

namespace MetricsAgentClient.Models
{
    public class Metric
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime DateTime { get; set; }
    }
}