namespace MyFishingApp.Web.Tests.Controllers
{
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.FishInputModels;
    using MyFishingApp.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class FishControllerTests
    {
        [Fact]
        public void GetFishByIdShouldReturnJsonString()
          => MyController<FishController>
          .Instance()
          .WithData(new Fish() { Id = "2151", Name = "White fish" })
          .Calling(c => c.GetFishById("2151"))
          .ShouldReturn()
          .ResultOfType<string>();

        [Fact]
        public void GetFishbyIdShouldThrowsExceptionWhenNoCountryIsFound()
          => MyController<FishController>
          .Instance()
          .WithData(new Fish() { Id = "2151", Name = "White fish" })
          .Calling(c => c.GetFishById("13526262623623"))
          .ShouldThrow()
           .Exception();

        [Fact]
        public void CreateFishShouldReturnOk()
            => MyController<FishController>
            .Instance()
            .Calling(c => c.CreateFish(new FishInputModel() { Name = "White fish", Lenght = 40, Weight = 10 }))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeleteFishShouldReturnOk()
            => MyController<FishController>
            .Instance()
             .WithData(new Fish() { Id = "2151", Name = "White fish" })
            .Calling(c => c.DeleteFish("2151"))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeleteFishShouldThrowsExceptionWhenNoFishIsFound()
           => MyController<FishController>
           .Instance()
           .WithData(new Fish() { Id = "2151", Name = "White fish" })
            .Calling(c => c.DeleteFish("21agadg513"))
           .ShouldThrow()
            .Exception();

        [Fact]
        public void GetAllFishShouldReturnJsonString()
            => MyController<FishController>
            .Instance()
            .WithData(new Fish() { Id = "2151", Name = "White fish" })
            .Calling(c => c.GetAllFish())
            .ShouldReturn()
            .ResultOfType<string>();

        [Fact]
        public void GetAllFishShouldReturnNullJsonStringWhenNoFishFound()
            => MyController<FishController>
            .Instance()
            .Calling(c => c.GetAllFish())
            .ShouldThrow()
            .Exception();

        [Fact]
        public void UpdateFishShouldReturnOk()
          => MyController<FishController>
          .Instance()
          .WithData(new Fish() { Id = "2151", Name = "White fish" })
          .Calling(c => c.UpdateFish(new UpdateFishInputModel() { Name = "Grey fish", Description = "Big fish", FishId = "2151", Habittat = "natural", Lenght = 100, Nutrition = "small fish", Weight = 20, Tips = null }))
          .ShouldReturn()
          .Ok();

        [Fact]
        public void UpdateFishShouldThrowExceptionWhenFishIsNotFound()
           => MyController<FishController>
           .Instance()
          .Calling(c => c.UpdateFish(new UpdateFishInputModel() { FishId = null, Name = "Grey fish" }))
           .ShouldReturn().BadRequest();

        [Fact]
        public void GetAllFishShouldHaveValidModelState()
            => MyController<FishController>
            .Instance()
            .WithData(new Fish() { Id = "2151", Name = "White fish" })
            .Calling(c => c.GetAllFish())
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void GetFishByIdShouldHaveValidModelState()
            => MyController<FishController>
            .Instance()
            .WithData(new Fish() { Id = "2151", Name = "White fish" })
            .Calling(c => c.GetFishById("2151"))
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void DeleteByIdShouldHaveValidModelState()
            => MyController<FishController>
            .Instance()
            .WithData(new Fish() { Id = "2151", Name = "White fish" })
            .Calling(c => c.DeleteFish("2151"))
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void GetFishByNameShouldReturnJsonString()
          => MyController<FishController>
          .Instance()
          .WithData(new Fish() { Id = "2151", Name = "White fish" })
          .Calling(c => c.GetFishByName("White fish"))
          .ShouldReturn()
          .ResultOfType<string>();

        [Fact]
        public void GetFishbyNameShouldThrowsExceptionWhenNoCountryIsFound()
          => MyController<FishController>
          .Instance()
          .WithData(new Fish() { Id = "2151", Name = "White fish" })
          .Calling(c => c.GetFishByName("13526262623623"))
          .ShouldThrow()
           .Exception();

        [Fact]
        public void GetFishByNameShouldHaveValidModelState()
         => MyController<FishController>
         .Instance()
        .WithData(new Fish() { Id = "2151", Name = "White fish" })
          .Calling(c => c.GetFishByName("White fish"))
         .ShouldHave()
         .ValidModelState();
    }
}
