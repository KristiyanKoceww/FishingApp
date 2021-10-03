namespace MyFishingApp.Services.Data.Weather
{
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models.WeatherForecast;
    using MyFishingApp.Services.Data.InputModels.WeatherInputModels;

    public interface IWeatherService
    {
        // Task GetWeather(WeatherInputModel weatherInputModel);
        Task GetWeather(WeatherForecast weatherForecast);

        Task UpdateWeather(WeatherInputModel weatherInputModel, string weatherId);
    }
}
