using Microsoft.AspNetCore.Mvc;
using MyFishingApp.Data.Models.WeatherForecast;
using MyFishingApp.Services.Data.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;



namespace MyFishingApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        public String WeatherDetail(string City)
        {

            //Assign API KEY which received from OPENWEATHERMAP.ORG  
            string appId = "12a4746d8befcc4c81d074e265b71afb";

            //API path with CITY parameter and other parameters.  
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", City, appId);

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                //********************//  
                //     JSON RECIVED   
                //********************//  
                //{"coord":{ "lon":72.85,"lat":19.01},  
                //"weather":[{"id":711,"main":"Smoke","description":"smoke","icon":"50d"}],  
                //"base":"stations",  
                //"main":{"temp":31.75,"feels_like":31.51,"temp_min":31,"temp_max":32.22,"pressure":1014,"humidity":43},  
                //"visibility":2500,  
                //"wind":{"speed":4.1,"deg":140},  
                //"clouds":{"all":0},  
                //"dt":1578730750,  
                //"sys":{"type":1,"id":9052,"country":"IN","sunrise":1578707041,"sunset":1578746875},  
                //"timezone":19800,  
                //"id":1275339,  
                //"name":"Mumbai",  
                //"cod":200}  
                
                var jsonString = JsonSerializer.Deserialize<WeatherForecast>(json);
                this.weatherService.GetWeather(jsonString);

                return json;
            }
           
        }

    }
}
}
