namespace MyFishingApp.Web.Tests.Controllers
{
    using System;

    using MyFishingApp.Services.Data.InputModels;
    using MyFishingApp.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class CityControllerTests
    {
        // https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/tree/development/samples/Blog/Blog.Test

        [Fact]
        public void GetAllCitiesShouldReturnJson()
            => MyController<CitiesController>
            .Instance()
            .Calling(c => c.GetAllCities())
            .ShouldReturn()
            .Json();

        [Fact]
        public void GetCityByNameShouldReturnJson()
            => MyController<CitiesController>
            .Instance()
            .Calling(c => c.GetCityByName("Sofia"))
            .ShouldReturn()
            .Json();

        [Fact]
        public void GetAllCityByIdShouldReturnJson()
            => MyController<CitiesController>
            .Instance()
            .Calling(c => c.GetCityById("1"))
            .ShouldReturn()
            .Json();

        [Fact]
        public void CreateCityShouldReturnOk()
            => MyController<CitiesController>
            .Instance()
            .Calling(c => c.CreateCity(With.Empty<CitiesInputModel>()))
            .ShouldReturn()
            .Ok();

        // Check if that test method fails to see what  does with.Empty

        [Fact]
        public void CreateCityShouldReturnJson()
           => MyController<CitiesController>
           .Instance(i => i.WithUser())
           .Calling(c => c.CreateCity(With.Empty<CitiesInputModel>()))
           .ShouldReturn()
           .Ok();

        [Fact]
        public void a()
            => MyController<CitiesController>
            .Instance()
            .Calling(c => c.GetAllCities())
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void GetAllCitiesRouth() =>
            MyRouting
            .Configuration()
            .ShouldMap("/api/Cities/getCities")
            .To<CitiesController>(c => c.GetAllCities());

        [Fact]
        public void CreateCityRouth() =>
           MyRouting
           .Configuration()
           .ShouldMap("/api/Cities/create")
           .To<CitiesController>(c => c.CreateCity(With.Empty<CitiesInputModel>()));

        [Fact]
        public void GetCityByIdRouth() =>
           MyRouting
           .Configuration()
           .ShouldMap("/api/Cities/getCityById/id")
           .To<CitiesController>(c => c.GetCityById(Guid.NewGuid().ToString()));

        [Fact]
        public void GetCityByNameRouth() =>
           MyRouting
           .Configuration()
           .ShouldMap("/api/Cities/getCityByName/Name")
           .To<CitiesController>(c => c.GetCityByName(Guid.NewGuid().ToString()));

        [Fact]
        public void GetAll()
        {
            var controller = MyController<CitiesController>
                .Instance();

            // Act
            var call = controller.Calling(c => c.GetCityById("1"));

            // Assert
            call
                .ShouldReturn()
                .StatusCode(200);
        }
    }
}
