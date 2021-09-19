namespace MyFishingApp.Services.Data.Weather
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.WeatherInputModels;

    public class WeatherService : IWeatherService
    {
        private readonly IDeletableEntityRepository<Weather> weatherRepository;

        public WeatherService(IDeletableEntityRepository<Weather> weatherRepository)
        {
            this.weatherRepository = weatherRepository;
        }

        public async Task GetWeather(WeatherInputModel weatherInputModel)
        {
            var weather = new Weather()
            {
                Temperature = weatherInputModel.Temperature,
                Main = weatherInputModel.Main,
                Latitude = weatherInputModel.Latitude,
                Longitude = weatherInputModel.Longitude,
                CityName = weatherInputModel.CityName,
                Wind = weatherInputModel.Wind,
                Cloudy = weatherInputModel.Cloudy,
                Humitity = weatherInputModel.Humitity,
            };

            if (weatherInputModel.Pressure is not null)
            {
                weather.Pressure = weatherInputModel.Pressure;
            }

            // THINK IF THIS IS THE MOST USEFUL WAY TO DO IT
            await this.weatherRepository.AddAsync(weather);
            await this.weatherRepository.SaveChangesAsync();
        }

        public async Task UpdateWeather(WeatherInputModel weatherInputModel, string weatherId)
        {
            var weather = this.weatherRepository.All().Where(x => x.Id == weatherId).FirstOrDefault();
            if (weather is not null)
            {
                weather.Temperature = weatherInputModel.Temperature;
                weather.Main = weatherInputModel.Main;
                weather.Latitude = weatherInputModel.Latitude;
                weather.Longitude = weatherInputModel.Longitude;
                weather.CityName = weatherInputModel.CityName;
                weather.Wind = weatherInputModel.Wind;
                weather.Cloudy = weatherInputModel.Cloudy;
                weather.Humitity = weatherInputModel.Humitity;
            }

            if (weatherInputModel.Pressure is not null)
            {
                weather.Pressure = weatherInputModel.Pressure;
            }

            this.weatherRepository.Update(weather);
            await this.weatherRepository.SaveChangesAsync();
        }
    }
}
