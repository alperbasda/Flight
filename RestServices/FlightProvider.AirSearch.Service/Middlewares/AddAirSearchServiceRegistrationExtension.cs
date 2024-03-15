using AirportWcfService;
using AirSearchWcfService;
using FlightProvider.AirSearch.Service.Business.Concretes;
using FlightProvider.AirSearch.Service.Business.Contracts;
using FlightProvider.AirSearch.Service.Business.Rules;
using System.Reflection;

namespace FlightProvider.AirSearch.Service.Middlewares;

public static class AddAirSearchServiceRegistrationExtension
{
    public static IServiceCollection AddAirSearchServiceRegistration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });


        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<AirServiceBusinessRules>();
        services.AddScoped<IAirSearchService, AirSearchService>();
        services.AddSingleton<AirSearchClient>();
        services.AddScoped<IAirportSearchService, AirportSearchService>();
        services.AddSingleton<AirportSearchClient>();

        return services;
    }
}
