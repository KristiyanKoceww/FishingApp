namespace MyFishingApp.Services.Data.Weather
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Data.Models.WeatherForecast;
    using MyFishingApp.Services.Data.InputModels.WeatherInputModels;

    public class WeatherService : IWeatherService
    {
        private readonly IDeletableEntityRepository<WeatherForecast> weatherForecastRepository;

        public WeatherService(
            IDeletableEntityRepository<WeatherForecast> weatherForecastRepository)
        {
            this.weatherForecastRepository = weatherForecastRepository;
        }

        public async Task GetWeather(WeatherForecast weatherForecast)
        {
            var weather = new WeatherForecast()
            {
                Base = weatherForecast.Base,
                Main =
                {
                     Temp = weatherForecast.Main.Temp,
                     TempMax = weatherForecast.Main.TempMax,
                     TempMin = weatherForecast.Main.TempMin,
                     FeelsLike = weatherForecast.Main.FeelsLike,
                     Humidity = weatherForecast.Main.Humidity,
                     Pressure = weatherForecast.Main.Pressure,
                },
                Coord =
                {
                    Lat = weatherForecast.Coord.Lat,
                    Lon = weatherForecast.Coord.Lon,
                },
                Clouds =
                {
                    All = weatherForecast.Clouds.All,
                },
                Wind =
                {
                     Speed = weatherForecast.Wind.Speed,
                     Deg = weatherForecast.Wind.Deg,
                },
                Visibility = weatherForecast.Visibility,
                Cod = weatherForecast.Cod,
                Dt = weatherForecast.Dt,
                Name = weatherForecast.Name,
                Sys =
                {
                    Country = weatherForecast.Sys.Country,
                    Type = weatherForecast.Sys.Type,
                    Sunrise = weatherForecast.Sys.Sunrise,
                    Sunset = weatherForecast.Sys.Sunset,
                },
                Timezone = weatherForecast.Timezone,
                Weather = weatherForecast.Weather,
            };

            await this.weatherForecastRepository.AddAsync(weatherForecast);
            await this.weatherForecastRepository.SaveChangesAsync();


            //var weather = new Weather1()
            //{
            //    Temperature = weatherInputModel.Temperature,
            //    Main = weatherInputModel.Main,
            //    Latitude = weatherInputModel.Latitude,
            //    Longitude = weatherInputModel.Longitude,
            //    CityName = weatherInputModel.CityName,
            //    Wind = weatherInputModel.Wind,
            //    Cloudy = weatherInputModel.Cloudy,
            //    Humitity = weatherInputModel.Humitity,
            //};

            //if (weatherInputModel.Pressure is not null)
            //{
            //    weather.Pressure = weatherInputModel.Pressure;
            //}

            //// THINK IF THIS IS THE MOST USEFUL WAY TO DO IT
            //await this.weatherRepository.AddAsync(weather);
            //await this.weatherRepository.SaveChangesAsync();
        }

        public async Task UpdateWeather(WeatherInputModel weatherInputModel, string weatherId)
        {
            //var weather = this.weatherForecastRepository.All().Where(x => x.Id == weatherId).FirstOrDefault();
            //if (weather is not null)
            //{
            //    weather.Temperature = weatherInputModel.Temperature;
            //    weather.Main = weatherInputModel.Main;
            //    weather.Latitude = weatherInputModel.Latitude;
            //    weather.Longitude = weatherInputModel.Longitude;
            //    weather.CityName = weatherInputModel.CityName;
            //    weather.Wind = weatherInputModel.Wind;
            //    weather.Cloudy = weatherInputModel.Cloudy;
            //    weather.Humitity = weatherInputModel.Humitity;
            //}

            //if (weatherInputModel.Pressure is not null)
            //{
            //    weather.Pressure = weatherInputModel.Pressure;
            //}

            //this.weatherRepository.Update(weather);
            //await this.weatherRepository.SaveChangesAsync();
        }
    }
}
