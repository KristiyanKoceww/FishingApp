namespace MyFishingApp.Web.Tests.Controllers
{
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;
    using MyFishingApp.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class KnotControllerTests
    {
        [Fact]
        public void GetKnotByIdShouldReturnJsonString()
          => MyController<KnotsController>
          .Instance()
          .WithData(new Knot() { Id = "2151", Name = "Knot" })
          .Calling(c => c.GetKnotById("2151"))
          .ShouldReturn()
          .ResultOfType<string>();

        [Fact]
        public void GetKnotbyIdShouldThrowsExceptionWhenNoKnotIsFound()
          => MyController<KnotsController>
          .Instance()
          .WithData(new Knot() { Id = "2151", Name = "Knot" })
          .Calling(c => c.GetKnotById("13526262623623"))
          .ShouldThrow()
           .Exception();

        [Fact]
        public void CreateKnotShouldReturnOk()
            => MyController<KnotsController>
            .Instance()
            .Calling(c => c.CreateKnot(new KnotInputModel() { Name = "Knot" }))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeleteKnotShouldReturnOk()
            => MyController<KnotsController>
            .Instance()
             .WithData(new Knot() { Id = "2151", Name = "Knot" })
            .Calling(c => c.DeleteKnot("2151"))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeleteKnotShouldThrowsExceptionWhenNoKnotIsFound()
           => MyController<KnotsController>
           .Instance()
           .WithData(new Knot() { Id = "2151", Name = "Knot" })
            .Calling(c => c.DeleteKnot("21agadg513"))
           .ShouldThrow()
            .Exception();

        [Fact]
        public void GetAllKnotShouldReturnJsonString()
            => MyController<KnotsController>
            .Instance()
            .WithData(new Knot() { Id = "2151", Name = "Knot" })
            .Calling(c => c.GetAllKnot())
            .ShouldReturn()
            .ResultOfType<string>();

        [Fact]
        public void GetAllKnotShouldReturnNullJsonStringWhenNoKnotFound()
            => MyController<KnotsController>
            .Instance()
            .Calling(c => c.GetAllKnot())
            .ShouldThrow()
            .Exception();

        [Fact]
        public void UpdateKnotShouldReturnOk()
          => MyController<KnotsController>
          .Instance()
          .WithData(new Knot() { Id = "2151", Name = "Knot" })
          .Calling(c => c.UpdateKnot(new KnotInputModel() { Name = "new Knot" }, "2151"))
          .ShouldReturn()
          .Ok();

        [Fact]
        public void UpdateKnotShouldThrowExceptionWhenKnotIsNotFound()
           => MyController<KnotsController>
           .Instance()
           .WithData(new Knot() { Id = "2151", Name = "White fish" })
          .Calling(c => c.UpdateKnot(new KnotInputModel() { Name = "New Knot" }, "2151341tatdgaga1"))
           .ShouldThrow()
            .Exception();

        [Fact]
        public void GetAllKnotsShouldHaveValidModelState()
            => MyController<KnotsController>
            .Instance()
            .WithData(new Knot() { Id = "2151", Name = "Knot" })
            .Calling(c => c.GetAllKnot())
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void GetKnotByIdShouldHaveValidModelState()
            => MyController<KnotsController>
            .Instance()
            .WithData(new Knot() { Id = "2151", Name = "Knot" })
            .Calling(c => c.GetKnotById("2151"))
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void DeleteByIdShouldHaveValidModelState()
            => MyController<KnotsController>
            .Instance()
            .WithData(new Knot() { Id = "2151", Name = "Knot" })
            .Calling(c => c.DeleteKnot("2151"))
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void GetKnotByNameShouldReturnJsonString()
          => MyController<KnotsController>
          .Instance()
          .WithData(new Knot() { Id = "2151", Name = "Knot233" })
          .Calling(c => c.GetKnotByName("Knot233"))
          .ShouldReturn()
          .ResultOfType<string>();

        [Fact]
        public void GetKnotbyNameShouldThrowsExceptionWhenNoKnotyIsFound()
          => MyController<KnotsController>
          .Instance()
          .WithData(new Knot() { Id = "2151", Name = "Knot" })
          .Calling(c => c.GetKnotByName("13526262623623"))
          .ShouldThrow()
           .Exception();

        [Fact]
        public void GetKnotByNameShouldHaveValidModelState()
         => MyController<KnotsController>
         .Instance()
        .WithData(new Knot() { Id = "2151", Name = "Knot233" })
          .Calling(c => c.GetKnotByName("Knot233"))
         .ShouldHave()
         .ValidModelState();
    }
}
