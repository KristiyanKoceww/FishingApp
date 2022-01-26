namespace MyFishingApp.Web.Tests.Controllers
{
    using System;

    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;
    using MyFishingApp.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class CityControllerTests
    {
        // https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/tree/development/samples/Blog/Blog.Test
        [Fact]
        public void GetAllCitiesShouldReturnJsonString()
            => MyController<CitiesController>
            .Instance()
            .WithData(new City() { Id = "2151", Name = "Varna" })
            .Calling(c => c.GetAllCities())
            .ShouldReturn()
            .ResultOfType<string>();

        [Fact]
        public void GetAllCitiesShouldReturnNullJsonStringWhenNoCitiesFound()
            => MyController<CitiesController>
            .Instance()
            .Calling(c => c.GetAllCities())
            .ShouldThrow()
            .Exception();

        [Fact]
        public void GetCityByNameShouldReturnJsonString()
            => MyController<CitiesController>
            .Instance()
            .WithData(new City() { Id = "2151", Name = "Varna" })
            .Calling(c => c.GetCityByName("Varna"))
            .ShouldReturn()
            .ResultOfType<string>();

        [Fact]
        public void GetCitybyNameShouldThrowsExceptionWhenNoCityIsFound()
           => MyController<CitiesController>
           .Instance()
           .WithData(new City() { Id = "2151", Name = "Varna" })
           .Calling(c => c.GetCityByName("myCity"))
           .ShouldThrow()
            .Exception();

        [Fact]
        public void GetCityByIdShouldReturnJsonString()
            => MyController<CitiesController>
            .Instance()
            .WithData(new City() { Id = "2151", Name = "Varna" })
            .Calling(c => c.GetCityById("2151"))
            .ShouldReturn()
            .ResultOfType<string>();

        [Fact]
        public void GetCitybyIdShouldThrowsExceptionWhenNoCityIsFound()
          => MyController<CitiesController>
          .Instance()
          .WithData(new City() { Id = "2151", Name = "Varna" })
          .Calling(c => c.GetCityByName("13526262623623"))
          .ShouldThrow()
           .Exception();

        [Fact]
        public void CreateCityShouldReturnOk()
            => MyController<CitiesController>
            .Instance()
            .WithData(new Country { Id = "1", Name = "BG" })
            .Calling(c => c.CreateCity(new CitiesInputModel() { Name = "Varna", Description = "Varna", Country = new Country() { Id = "1", Name = "BG" }, CountryId = "1" }))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void CreateCityShouldThrowExceptionWhenArgumentIsMissing()
            => MyController<CitiesController>
            .Instance()
            .Calling(c => c.CreateCity(new CitiesInputModel() { Name = "Varna", Description = "Varna" }))
            .ShouldReturn().BadRequest();

        [Fact]
        public void DeleteCityShouldReturnOk()
            => MyController<CitiesController>
            .Instance()
            .WithData(new City() { Id = "2151", Name = "Varna" })
            .Calling(c => c.DeleteCity("2151"))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeleteCityShouldThrowsExceptionWhenNoCityIsFound()
           => MyController<CitiesController>
           .Instance()
           .WithData(new City() { Id = "2151", Name = "Varna" })
           .Calling(c => c.DeleteCity("215111"))
           .ShouldThrow()
            .Exception();

        [Fact]
        public void GetAllCitiesShouldHaveValidModelState()
            => MyController<CitiesController>
            .Instance()
            .WithData(new City() { Id = "2151", Name = "Varna" })
            .Calling(c => c.GetAllCities())
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void GetCityByIdShouldHaveValidModelState()
            => MyController<CitiesController>
            .Instance()
            .WithData(new City() { Id = "2151", Name = "Varna" })
            .Calling(c => c.GetCityById("2151"))
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void GetCityByNameShouldHaveValidModelState()
          => MyController<CitiesController>
          .Instance()
          .WithData(new City() { Id = "2151", Name = "Varna" })
          .Calling(c => c.GetCityByName("Varna"))
          .ShouldHave()
          .ValidModelState();

        [Fact]
        public void GetAllCitiesRouth() =>
            MyRouting
            .Configuration()
            .ShouldMap("/api/Cities/getCities")
            .To<CitiesController>(c => c.GetAllCities());

        [Fact]
        public void UpdateCityShouldReturnOk()
           => MyController<CitiesController>
           .Instance()
           .WithData(new City() { Id = "2151", Name = "Varna" })
           .Calling(c => c.UpdateCity("2151", new CitiesInputModel() { Name = "Sofia", Description = "Super", Country = new Country() { Id = "1", Name = "BG" } }))
           .ShouldReturn()
           .Ok();

        [Fact]
        public void UpdateCityShouldThrowExceptionWhenCityIsNotFound()
           => MyController<CitiesController>
           .Instance()
           .WithData(new City() { Id = "123153563252", Name = "Varna" })
           .Calling(c => c.UpdateCity("2151", new CitiesInputModel() { Name = "Sofia", Description = "Super", Country = new Country() { Id = "1", Name = "BG" } }))
           .ShouldThrow()
            .Exception();

        [Fact]
        public void UpdateCityShouldThrowExceptionWhenOneArgumentIsMissing()
           => MyController<CitiesController>
           .Instance()
           .WithData(new City() { Id = "123153563252", Name = "Varna" })
           .Calling(c => c.UpdateCity("2151", new CitiesInputModel() { Name = "Sofia", Description = "Super" }))
           .ShouldThrow()
            .Exception();
    }
}
