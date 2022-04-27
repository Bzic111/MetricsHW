using AutoMapper;
using MetricsAgent.DAL.DTO;
using MetricsAgent.DAL.Models;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CpuMetric, CpuMetricDTO>().ReverseMap();
        CreateMap<RamMetrics, RamMetricsDTO>().ReverseMap();
        CreateMap<HddMetrics, HddMetricsDTO>().ReverseMap();
        CreateMap<DotNetMetrics, DotNetMetricsDTO>().ReverseMap();
        CreateMap<NetworkMetrics, NetworkMetricsDTO>().ReverseMap();
    }
}
//public class StringToDateTimeConverter : ITypeConverter<string, DateTime>
//{
//    public DateTime Convert(ResolutionContext context)
//    {
//        object objDateTime = context.SourceValue;
//        DateTime dateTime;

//        if (objDateTime == null)
//        {
//            return default(DateTime);
//        }

//        if (DateTime.TryParse(objDateTime.ToString(), out dateTime))
//        {
//            return dateTime;
//        }

//        return default(DateTime);
//    }
//}
