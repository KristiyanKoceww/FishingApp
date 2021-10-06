namespace MyFishingApp.Web.Tests.Controllers
{
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.CommentsInputModels;
    using MyFishingApp.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class CommentControllerTests
    {
        [Fact]
        public void CreateCommentShouldReturnOk()
           => MyController<CommentsController>
           .Instance()
            .WithData(new Post() { Id = 1, Content = "my content", Title = "my title" })
           .Calling(c => c.CreateComment(new CommentsInputModel() { Content = "my content", PostId = 1, ParentId = null }))
           .ShouldReturn()
           .Ok();

        [Fact]
        public void UpdateCommentShouldReturnOk()
           => MyController<CommentsController>
           .Instance()
            .WithData(new Post() { Id = 1, Content = "my content", Title = "my title" })
            .WithData(new Comment() { Id = 1, Content = "my content", PostId = 1, UserId = "421" })
           .Calling(c => c.UpdateComment(1, new CommentsInputModel() { Content = "my content22", PostId = 1, ParentId = null }))
           .ShouldReturn()
           .Ok();

        [Fact]
        public void UpdateCommentShouldThrowExceptionWhenCommentIsNotFound()
           => MyController<CommentsController>
           .Instance()
            .WithData(new Post() { Id = 1, Content = "my content", Title = "my title" })
            .WithData(new Comment() { Id = 1, Content = "my content", PostId = 1, UserId = "421" })
           .Calling(c => c.UpdateComment(2, new CommentsInputModel() { Content = "my content22", PostId = 1, ParentId = null }))
           .ShouldThrow()
            .Exception();

        [Fact]
        public void UpdateCommentShouldThrowExceptionWhenPostIsNull()
          => MyController<CommentsController>
          .Instance()
           .WithData(new Comment() { Id = 1, Content = "my content", PostId = 1, UserId = "421" })
          .Calling(c => c.UpdateComment(1, new CommentsInputModel() { Content = "my content22", PostId = 1, ParentId = null }))
          .ShouldThrow()
           .Exception();

        [Fact]
        public void DeleteCommentShouldReturnOk()
            => MyController<CommentsController>
            .Instance()
              .WithData(new Post() { Id = 1, Content = "my content", Title = "my title" })
             .WithData(new Comment() { Id = 1, Content = "my content", PostId = 1, UserId = "421" })
            .Calling(c => c.DeleteComment(1))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeleteCommenShouldThrowsExceptionWhenNoCommentIsFound()
           => MyController<CommentsController>
           .Instance()
            .WithData(new Post() { Id = 1, Content = "my content", Title = "my title" })
             .WithData(new Comment() { Id = 1, Content = "my content", PostId = 1, UserId = "421" })
            .Calling(c => c.DeleteComment(00))
           .ShouldThrow()
            .Exception();
    }
}
