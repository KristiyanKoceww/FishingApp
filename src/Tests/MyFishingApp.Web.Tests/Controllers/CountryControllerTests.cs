namespace MyFishingApp.Web.Tests.Controllers
{
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;
    using MyFishingApp.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class CountryControllerTests
    {
        [Fact]
        public void GetCountryByIdShouldReturnJsonString()
           => MyController<CountriesController>
           .Instance()
           .WithData(new Country() { Id = "2151", Name = "BG" })
           .Calling(c => c.GetCountryById("2151"))
           .ShouldReturn()
           .ResultOfType<string>();

        [Fact]
        public void GetCountrybyIdShouldThrowsExceptionWhenNoCountryIsFound()
          => MyController<CountriesController>
          .Instance()
          .WithData(new Country() { Id = "2151", Name = "BG" })
          .Calling(c => c.GetCountryById("13526262623623"))
          .ShouldThrow()
           .Exception();

        [Fact]
        public void CreateCountryShouldReturnOk()
            => MyController<CountriesController>
            .Instance()
            .Calling(c => c.CreateCountry(new CountryInputModel() { Name = "BG" }))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeleteCountryShouldReturnOk()
            => MyController<CountriesController>
            .Instance()
            .WithData(new Country() { Id = "2151", Name = "BG" })
            .Calling(c => c.DeleteCountry("2151"))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeleteCountryShouldThrowsExceptionWhenNoCountryIsFound()
           => MyController<CountriesController>
           .Instance()
           .WithData(new Country() { Id = "2151", Name = "BG" })
            .Calling(c => c.DeleteCountry("21agadg513"))
           .ShouldThrow()
            .Exception();

        [Fact]
        public void GetAllCountriesShouldReturnJsonString()
            => MyController<CountriesController>
            .Instance()
            .WithData(new Country() { Id = "2151", Name = "BG" })
            .Calling(c => c.GetAllCountries())
            .ShouldReturn()
            .ResultOfType<string>();

        [Fact]
        public void GetAllCountriesShouldReturnNullJsonStringWhenNoCountriesFound()
            => MyController<CountriesController>
            .Instance()
            .Calling(c => c.GetAllCountries())
            .ShouldThrow()
            .Exception();

        [Fact]
        public void UpdateCountryShouldReturnOk()
          => MyController<CountriesController>
          .Instance()
          .WithData(new Country() { Id = "2151", Name = "BG" })
          .Calling(c => c.UpdateCountry("2151", "GR"))
          .ShouldReturn()
          .Ok();

        [Fact]
        public void UpdateCityShouldThrowExceptionWhenCityIsNotFound()
           => MyController<CountriesController>
           .Instance()
           .WithData(new Country() { Id = "2151", Name = "BG" })
          .Calling(c => c.UpdateCountry("215sghdhsd2352531", "GR"))
           .ShouldThrow()
            .Exception();

        [Fact]
        public void GetAllCountriesShouldHaveValidModelState()
            => MyController<CountriesController>
            .Instance()
            .WithData(new Country() { Id = "2151", Name = "BG" })
            .Calling(c => c.GetAllCountries())
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void GetCountryByIdShouldHaveValidModelState()
            => MyController<CountriesController>
            .Instance()
            .WithData(new Country() { Id = "2151", Name = "BG" })
            .Calling(c => c.GetCountryById("2151"))
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void DeleteByIdShouldHaveValidModelState()
            => MyController<CountriesController>
            .Instance()
            .WithData(new Country() { Id = "2151", Name = "BG" })
            .Calling(c => c.DeleteCountry("2151"))
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void GetAllCountriesRouth() =>
            MyRouting
            .Configuration()
            .ShouldMap("/api/Countries/getCountries")
            .To<CountriesController>(c => c.GetAllCountries());

        [Fact]
        public void UpdateShouldHaveValidModelState()
         => MyController<CountriesController>
         .Instance()
         .WithData(new Country() { Id = "2151", Name = "BG" })
          .Calling(c => c.UpdateCountry("2151", "GR"))
         .ShouldHave()
         .ValidModelState();
    }
}
