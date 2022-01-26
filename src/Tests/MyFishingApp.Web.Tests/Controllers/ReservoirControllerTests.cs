namespace MyFishingApp.Web.Tests.Controllers
{
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;
    using MyFishingAppReact.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ReservoirControllerTests
    {
        [Fact]
        public void GetReservoirByIdShouldReturnJsonString()
          => MyController<ReservoirController>
          .Instance()
          .WithData(new Reservoir() { Id = "2151", Name = "Iskar" })
          .Calling(c => c.GetReservoirById("2151"))
          .ShouldReturn()
          .ResultOfType<string>();

        [Fact]
        public void GetReservoirbyIdShouldThrowsExceptionWhenNoReservoirIsFound()
          => MyController<ReservoirController>
          .Instance()
          .WithData(new Reservoir() { Id = "2151", Name = "Iskar" })
          .Calling(c => c.GetReservoirById("13526262623623"))
          .ShouldThrow()
           .Exception();

        [Fact]
        public void CreateReservoirShouldReturnOk()
            => MyController<ReservoirController>
            .Instance()
            .Calling(c => c.CreateReservoir(new CreateReservoirInputModel() { Name = "Knot" }))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeleteReservoirShouldReturnOk()
            => MyController<ReservoirController>
            .Instance()
             .WithData(new Reservoir() { Id = "2151", Name = "Iskar" })
            .Calling(c => c.DeleteReservoir("2151"))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeleteReservoirShouldThrowsExceptionWhenNoReservoirIsFound()
           => MyController<ReservoirController>
           .Instance()
           .WithData(new Reservoir() { Id = "2151", Name = "Iskar" })
            .Calling(c => c.DeleteReservoir("21agadg513"))
           .ShouldThrow()
            .Exception();

        [Fact]
        public void GetAllReservoirShouldReturnJsonString()
            => MyController<ReservoirController>
            .Instance()
             .WithData(new Reservoir() { Id = "2151", Name = "Iskar" })
            .Calling(c => c.GetAllReservoirs())
            .ShouldReturn()
            .ResultOfType<string>();

        [Fact]
        public void GetAllReservoirShouldReturnNullJsonStringWhenNoReservoirFound()
            => MyController<ReservoirController>
            .Instance()
            .Calling(c => c.GetAllReservoirs())
            .ShouldThrow()
            .Exception();

        [Fact]
        public void UpdateReservoirShouldReturnOk()
          => MyController<ReservoirController>
          .Instance()
           .WithData(new Reservoir() { Id = "2151", Name = "Iskar" })
          .Calling(c => c.UpdateReservoir(new UpdateReservoirInputModel() {ReservoirId= "2151", Name = "new Iskar", Description = "gaga", Latitude = "124", Longitude = "122", Type = "big" }))
          .ShouldReturn()
          .Ok();

        [Fact]
        public void UpdateReservoirShouldThrowExceptionWhenReservoirIsNotFound()
           => MyController<ReservoirController>
           .Instance()
           .WithData(new Reservoir() { Id = "2151", Name = "Iskar" })
          .Calling(c => c.UpdateReservoir(new UpdateReservoirInputModel() { Name = "new Iskar" }))
           .ShouldReturn().BadRequest();

        [Fact]
        public void GetAllReservoirsShouldHaveValidModelState()
            => MyController<ReservoirController>
            .Instance()
            .WithData(new Reservoir() { Id = "2151", Name = "Iskar" })
            .Calling(c => c.GetAllReservoirs())
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void GetReservoirByIdShouldHaveValidModelState()
            => MyController<ReservoirController>
            .Instance()
            .WithData(new Reservoir() { Id = "2151", Name = "Iskar" })
            .Calling(c => c.GetReservoirById("2151"))
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void DeleteByIdShouldHaveValidModelState()
            => MyController<ReservoirController>
            .Instance()
            .WithData(new Reservoir() { Id = "2151", Name = "Iskar" })
            .Calling(c => c.DeleteReservoir("2151"))
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void GetReservoirByNameShouldReturnJsonString()
          => MyController<ReservoirController>
          .Instance()
         .WithData(new Reservoir() { Id = "2151", Name = "Iskar" })
          .Calling(c => c.GetReservoirByName("Iskar"))
          .ShouldReturn()
          .ResultOfType<string>();

        [Fact]
        public void GetReservoirbyNameShouldThrowsExceptionWhenNoReservoirIsFound()
          => MyController<ReservoirController>
          .Instance()
          .WithData(new Reservoir() { Id = "2151", Name = "Iskar" })
          .Calling(c => c.GetReservoirByName("13526262623623"))
          .ShouldThrow()
           .Exception();

        [Fact]
        public void GetReservoirByNameShouldHaveValidModelState()
         => MyController<ReservoirController>
         .Instance()
        .WithData(new Reservoir() { Id = "2151", Name = "Iskar" })
          .Calling(c => c.GetReservoirByName("Iskar"))
         .ShouldHave()
         .ValidModelState();
    }
}
