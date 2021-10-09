namespace MyFishingApp.Web.Tests.Controllers
{
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.VoteInputModels;
    using MyFishingApp.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class VoteControllerTests
    {
        [Fact]
        public void VoteShouldReturnOk()
        => MyController<VotesController>
        .Instance()
        .WithData(new ApplicationUser() { Id = "1", Age = 17, FirstName = "User", MiddleName = "user", LastName = "user", Email = "user@gmail.com", Gender = Gender.Female, UserName = "userName", Phone = "3598534421" })
        .WithData(new Post() { Id = 1, UserId = "1", Content = "content", Title = "title" })
        .WithData(new Vote() { Id = 1, PostId = 1, Type = VoteType.Neutral, UserId = "1" })
        .Calling(c => c.Vote(new VoteInputModel() { IsUpVote = true, PostId = 1, UserId = "1" }))
        .ShouldReturn()
       .Ok();

        [Fact]
        public void VoteCountShouldReturnInt()
        => MyController<VotesController>
        .Instance()
        .WithData(new ApplicationUser() { Id = "1", Age = 17, FirstName = "User", MiddleName = "user", LastName = "user", Email = "user@gmail.com", Gender = Gender.Female, UserName = "userName", Phone = "3598534421" })
        .WithData(new ApplicationUser() { Id = "2", Age = 17, FirstName = "User", MiddleName = "user", LastName = "user", Email = "user@gmail.com", Gender = Gender.Female, UserName = "userName", Phone = "3598534421" })
        .WithData(new Post() { Id = 1, UserId = "1", Content = "content", Title = "title" })
        .WithData(new Vote() { Id = 1, PostId = 1, Type = VoteType.Neutral, UserId = "1" })
        .WithData(new Vote() { Id = 2, PostId = 1, Type = VoteType.UpVote, UserId = "2" })
        .Calling(c => c.GetVotes(1))
        .ShouldReturn()
        .ResultOfType<int>();
    }
}
