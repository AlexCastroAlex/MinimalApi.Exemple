namespace MinimalApiExemple.ForecastService
{
    public interface IForecastService
    {
        Task<WeatherForecast[]> GetAllForecastForecastsAsync();
    }

    public class ForecastService : IForecastService
    {
        private readonly string[] summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

        public async Task<WeatherForecast[]> GetAllForecastForecastsAsync()
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();

            return await Task.FromResult(forecast);
        }

    }

    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
