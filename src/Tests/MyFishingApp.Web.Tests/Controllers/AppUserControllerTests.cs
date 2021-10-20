namespace MyFishingApp.Web.Tests.Controllers
{
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.AppUsersInputModels;
    using MyFishingApp.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class AppUserControllerTests
    {
        [Fact]
        public void GetUserByIdShouldReturnJsonString()
        => MyController<AppUsersController>
        .Instance()
        .WithData(new ApplicationUser() { Id = "2", Age = 17, FirstName = "User" })
        .Calling(c => c.GetUserById("2"))
        .ShouldReturn()
        .ResultOfType<string>();

        [Fact]
        public void GetUserbyIdShouldThrowsExceptionWhenNoUserIsFound()
          => MyController<AppUsersController>
          .Instance()
          .WithData(new ApplicationUser() { Id = "2", Age = 17, FirstName = "User" })
          .Calling(c => c.GetUserById("userId"))
          .ShouldThrow()
           .Exception();

        [Fact]
        public void CreateUserShouldReturnOk()
            => MyController<AppUsersController>
            .Instance()
            .Calling(c => c.Register(new UserInputModel() { Age = 17, FirstName = "User", MiddleName = "user" , LastName = "user", Email = "user@gmail.com" , Gender = Gender.Female, Password = "1234", UserName = "userName", PhoneNumber = "3598534421" }))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeleteUserShouldReturnOk()
            => MyController<AppUsersController>
            .Instance()
              .WithData(new ApplicationUser() { Id = "2", Age = 17, FirstName = "User" })
            .Calling(c => c.DeleteUser("2"))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void DeleteUserShouldThrowsExceptionWhenNoPostIsFound()
           => MyController<AppUsersController>
           .Instance()
           .WithData(new ApplicationUser() { Id = "2", Age = 17, FirstName = "User" })
            .Calling(c => c.DeleteUser("userId"))
           .ShouldThrow()
            .Exception();

        [Fact]
        public void UpdateUserShouldReturnOk()
          => MyController<AppUsersController>
          .Instance()
           .WithData(new ApplicationUser() { Id = "2", Age = 17, FirstName = "User" })
          .Calling(c => c.UpdateUser(new UserInputModel() { Age = 17, FirstName = "User", MiddleName = "user", LastName = "user", Email = "user@gmail.com", Gender = Gender.Female, Password = "1234", UserName = "userName", PhoneNumber = "3598534421" }, "2"))
          .ShouldReturn()
          .Ok();

        [Fact]
        public void UpdateUserShouldThrowExceptionWhenUserIsNotFound()
           => MyController<AppUsersController>
           .Instance()
            .WithData(new ApplicationUser() { Id = "2", Age = 17, FirstName = "User" })
          .Calling(c => c.UpdateUser(new UserInputModel() { Age = 17, FirstName = "User" }, "3"))
           .ShouldThrow()
            .Exception();

        [Fact]
        public void GetUserByIdShouldHaveValidModelState()
            => MyController<AppUsersController>
            .Instance()
             .WithData(new ApplicationUser() { Id = "2", Age = 17, FirstName = "User" })
            .Calling(c => c.GetUserById("2"))
            .ShouldHave()
            .ValidModelState();
    }
}
