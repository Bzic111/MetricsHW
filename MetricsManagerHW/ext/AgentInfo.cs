namespace MetricsManagerHW.ext
{
    public class AgentInfo
    {
        public int AgentId { get; set; }
        public string AgentAddress { get; set; }
        public AgentInfo(int id, string addres)
        {
            AgentId = id;
            AgentAddress = addres;
        }
    }
}