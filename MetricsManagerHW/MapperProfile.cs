using AutoMapper;
using MetricsManagerHW.DAL.DTO;
using MetricsManagerHW.DAL.Models;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Agent, AgentDTO>().ReverseMap();
        CreateMap<CpuMetric, CpuMetricDTO>().ReverseMap();
        CreateMap<RamMetrics, RamMetricsDTO>().ReverseMap();
        CreateMap<HddMetrics, HddMetricsDTO>().ReverseMap();
        CreateMap<DotNetMetrics, DotNetMetricsDTO>().ReverseMap();
        CreateMap<NetworkMetrics, NetworkMetricsDTO>().ReverseMap();
    }
}