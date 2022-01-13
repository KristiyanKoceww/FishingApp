namespace MyFishingApp.Services.Data.AppUsers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.AppUsersInputModels;

    public interface IAppUser
    {
        Task CreateAsync(UserInputModel userInputModel);

        Task DeleteAsync(string userId);

        Task<ApplicationUser> UpdateUserAsync(UpdateUserInfoInputModel userInputModel, string userId);

        Task ChangeUserProfilePicture(ChangePictureInputModel changePictureInputModel);

        ApplicationUser GetById(string userId);

        ApplicationUser GetByUsername(string username);

        UserInfo GetUserInfo(string userId);

        ApplicationUser Authenticate(string username, string password);
    }
}
