namespace MetricsManagerHW.ext;
public class AgentInfo
{
    public int AgentId { get; set; }
    public Uri AgentAddress { get; set; }
    public AgentInfo(int id, Uri uri)
    {
        AgentId = id;
        AgentAddress = uri;
    }
}
