using AirSearchWcfService;
using AutoMapper;
using FlightProvider.AirSearch.Service.Models.Requests;
using FlightProvider.AirSearch.Service.Models.Responses.Poco;

namespace FlightProvider.AirSearch.Service.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FlightOption, DetailSearchResultDto>()
        .ForMember(q => q.GroupId, w => w.MapFrom(r => 0));

        CreateMap<FlightOption, ListSearchResultDto>()
            .ForMember(q => q.GroupId, w => w.MapFrom(r => 0));

        CreateMap<SearchRequestDto, SearchRequest>();
        CreateMap<GetByFlightNumberRequestDto, SearchRequest>();

        CreateMap<GetByFlightNumberRequestDto, SearchRequestDto>()
            .ForMember(q => q.GroupId, w => w.MapFrom(r => 0));



    }
}
