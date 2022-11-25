using Microsoft.AspNetCore.Mvc;
using MinimalApiExemple.Extensions;
using MinimalApiExemple.ForecastService;

namespace MinimalApiExemple.Endpoints
{
    public class WeatherForecastEndpoint : IEndpoint
    {
        public static void Register(IEndpointRouteBuilder endpoints)
        {
            
            var forecast = endpoints.MapGroup("WeatherForecast")
            .WithTags("WeatherForecast");

            _ = forecast
            .MapGet(
                "/weatherforecast",
                async (IForecastService service) =>
                {
                    
                    return await service.GetAllForecastForecastsAsync();
                })
            .Produces<WeatherForecast>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithName("Vocabulaire query")
            .WithSummary("Requête de récupération des données météo")
            .WithDescription("Requête de récupération des données météo")
            .WithOpenApi(); ;
        }
    }
}
