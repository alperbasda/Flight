using AirSearchWcfService;
using AutoMapper;
using FlightProvider.AirSearch.Service.Business.Contracts;
using FlightProvider.AirSearch.Service.Business.Rules;
using FlightProvider.AirSearch.Service.Models.Requests;
using FlightProvider.AirSearch.Service.Models.Responses;
using FlightProvider.AirSearch.Service.Models.Responses.Poco;
using System.Collections.Generic;

namespace FlightProvider.AirSearch.Service.Business.Concretes;

public class AirSearchService : IAirSearchService
{
    private readonly AirSearchClient _airSearchClient;
    private readonly IMapper _mapper;
    private readonly AirServiceBusinessRules _airServiceBusinessRules;
    public AirSearchService(AirSearchClient airSearchClient, IMapper mapper, AirServiceBusinessRules airServiceBusinessRules)
    {
        _airSearchClient = airSearchClient;
        _mapper = mapper;
        _airServiceBusinessRules = airServiceBusinessRules;
    }

    public async Task<ApiResponse<List<ListSearchResultDto>>> AvailabilitySearchAsync(List<SearchRequestDto> request)
    {
        request.ForEach(_airServiceBusinessRules.ThrowExceptionIfDepartureDateLessThanNow);

        var tasks = request.Select(GetAvailableFlights<ListSearchResultDto>);
        var result = await Task.WhenAll(tasks);

        return ApiResponse<List<ListSearchResultDto>>.Success(result.SelectMany(q => q).ToList(), 200);
    }

    public async Task<ApiResponse<DetailSearchResultDto>> Detail(GetByFlightNumberRequestDto request)
    {
        var result = await GetAvailableFlights<DetailSearchResultDto>(_mapper.Map<SearchRequestDto>(request));
        var selectedResult = result.FirstOrDefault(q => q.FlightNumber == request.FlightNumber);

        _airServiceBusinessRules.ThrowExceptionIfDataNull(selectedResult);

        return ApiResponse<DetailSearchResultDto>.Success(selectedResult!, 200);

    }

    private async Task<List<T>> GetAvailableFlights<T>(SearchRequestDto request)
        where T : class, IFlightResponse
    {
        var result = await _airSearchClient.AvailabilitySearchAsync(_mapper.Map<SearchRequest>(request));

        //Burada iş akışına göre log atılıp işleme devam edilebilir. Veya şuan yapılan şekilde exception fırlatılıp akış kesilebilir.
        if (result == null || result.HasError)
        {
            throw new Exception("Servisten gelen veride hata var.");
        }
        var res = _mapper.Map<List<T>>(result.FlightOptions);
        res.ForEach(q => q.GroupId = request.GroupId);
        return res;
    }
}
