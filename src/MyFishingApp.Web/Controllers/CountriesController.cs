using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFishingApp.Services.Data.Countries;
using MyFishingApp.Services.Data.InputModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFishingApp.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {

        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCountry(CountryInputModel countryInputModel)
        {
            await this.countryService.CreateAsync(countryInputModel);

            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCountry(string countryId, string countryName)
        {
            await this.countryService.UpdateAsync(countryId, countryName);

            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteCountry(string countryId)
        {
            await this.countryService.DeleteCountryAsync(countryId);

            return Ok();
        }

        [HttpGet("getCountries")]
        public string GetAllCountries()
        {
            var countries = this.countryService.GetAll();

            var json = JsonConvert.SerializeObject(countries);

            return json;
        }

        [HttpGet("getCountryById/id")]
        public string GetCountryById(string countryId)
        {
            var country = this.countryService.FindCountryById(countryId);

            var json = JsonConvert.SerializeObject(country);

            return json;
        }


    }
}
