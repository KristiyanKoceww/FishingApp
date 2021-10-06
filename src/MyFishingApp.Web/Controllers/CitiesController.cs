using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFishingApp.Data.Models;
using MyFishingApp.Services.Data.Cities;
using MyFishingApp.Services.Data.InputModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFishingApp.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService cityService;

        public CitiesController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCity(CitiesInputModel citiesInputModel)
        {
            await this.cityService.CreateAsync(citiesInputModel);

            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteCity(string cityId)
        {
            await this.cityService.DeleteAsync(cityId);

            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCity(string cityId, CitiesInputModel citiesInputModel)
        {
            await this.cityService.UpdateAsync(cityId, citiesInputModel);

            return Ok();
        }

        [HttpGet("getCities")]
        public string GetAllCities()
        {
            var cities = this.cityService.GetAllCities();

            var json = JsonConvert.SerializeObject(cities);

            return json;
        }

        [HttpGet("getCityById/id")]
        public string GetCityById(string cityId)
        {
            var city = this.cityService.FindCityById(cityId);

            var json = JsonConvert.SerializeObject(city);

            return json;
        }

        [HttpGet("getCityByName/Name")]
        public string GetCityByName(string cityName)
        {
            var city = this.cityService.FindCityByName(cityName);

            var json = JsonConvert.SerializeObject(city);

            return json;
        }
    }
}
