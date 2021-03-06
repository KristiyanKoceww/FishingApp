namespace MyFishingApp.Web.Tests.Controllers
{
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.PostInputModels;
    using MyFishingApp.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class PostControllerTests
    {
        [Fact]
        public void GetPostByIdShouldReturnJsonString()
          => MyController<PostsController>
          .Instance()
          .WithData(new Post() { Id = 1, Content = "my content", Title = "my title" })
          .Calling(c => c.GetPostById(1))
          .ShouldReturn()
          .ResultOfType<string>();

        [Fact]
        public void GetPostbyIdShouldThrowsExceptionWhenNoPostIsFound()
          => MyController<PostsController>
          .Instance()
          .WithData(new Post() { Id = 1, Content = "my content", Title = "my title" })
          .Calling(c => c.GetPostById(0316262))
          .ShouldReturn().ResultOfType<string>();

        [Fact]
        public void CreatePostShouldReturnOk()
            => MyController<PostsController>
            .Instance()
            .WithData(new ApplicationUser() { Id = "1" })
            .Calling(c => c.CreatePost(new CreatePostInputModel() { UserId = "1", Content = "my content", Title = "my title" }))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeletePostShouldReturnOk()
            => MyController<PostsController>
            .Instance()
              .WithData(new Post() { Id = 1, Content = "my content", Title = "my title" })
            .Calling(c => c.DeletePost(1))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeletePostShouldThrowsExceptionWhenNoPostIsFound()
           => MyController<PostsController>
           .Instance()
            .WithData(new Post() { Id = 1, Content = "my content", Title = "my title" })
            .Calling(c => c.DeletePost(00))
           .ShouldThrow()
            .Exception();

        [Fact]
        public void UpdatePostShouldReturnOk()
          => MyController<PostsController>
          .Instance()
          .WithData(new Post() { Id = 1, Content = "my content", Title = "my title" })
          .Calling(c => c.UpdatePost(new UpdatePostInputModel() { PostId = 1, Content = "my content222", Title = "my title222" }))
          .ShouldReturn()
          .Ok();

        [Fact]
        public void UpdatePostShouldThrowExceptionWhenPostIsNotFound()
           => MyController<PostsController>
           .Instance()
            .WithData(new Post() { Id = 1, Content = "my content", Title = "my title" })
          .Calling(c => c.UpdatePost(new UpdatePostInputModel() { Content = "my content222", Title = "my title222" }))
           .ShouldReturn().BadRequest();

        [Fact]
        public void GetPostByIdShouldHaveValidModelState()
            => MyController<PostsController>
            .Instance()
             .WithData(new Post() { Id = 1, Content = "my content", Title = "my title", Comments = null, ImageUrls = null, UserId = "1", })
            .Calling(c => c.GetPostById(1))
            .ShouldHave()
            .ValidModelState();

        [Fact]
        public void DeleteByIdShouldHaveValidModelState()
            => MyController<PostsController>
            .Instance()
            .WithData(new Post() { Id = 1, Content = "my content", Title = "my title" })
            .Calling(c => c.DeletePost(1))
            .ShouldHave()
            .ValidModelState();
    }
}
