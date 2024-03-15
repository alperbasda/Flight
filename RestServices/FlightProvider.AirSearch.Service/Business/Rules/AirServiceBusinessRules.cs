using FlightProvider.AirSearch.Service.Exceptions;
using FlightProvider.AirSearch.Service.Models.Requests;

namespace FlightProvider.AirSearch.Service.Business.Rules;

public class AirServiceBusinessRules
{
    public void ThrowExceptionIfDataNull<T>(T request)
    {
        if (request == null)
        {
            throw new BusinessException($"Uçuş Bulunamadı.");
        }
    }

    public void ThrowExceptionIfDepartureDateLessThanNow(SearchRequestDto request)
    {
        if (request.DepartureDate <= DateTime.Now)
        {
            throw new BusinessException($"Kalkış Tarihi {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} Tarihinden Küçük Olamaz.");
        }
    }
}
