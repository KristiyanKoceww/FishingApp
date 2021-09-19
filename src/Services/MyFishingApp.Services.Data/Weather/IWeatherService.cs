namespace MyFishingApp.Services.Data.Weather
{
    using System.Threading.Tasks;

    using MyFishingApp.Services.Data.InputModels.WeatherInputModels;

    public interface IWeatherService
    {
        Task GetWeather(WeatherInputModel weatherInputModel);

        Task UpdateWeather(WeatherInputModel weatherInputModel, string weatherId);
    }
}
