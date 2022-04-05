namespace MetricsManagerHW.ext;
public class AgentInfo
{
    public int AgentId { get; }
    public Uri AgentAddress { get; }
    public AgentInfo(int id, Uri uri)
    {
        AgentId = id;
        AgentAddress = uri;
    }
}
