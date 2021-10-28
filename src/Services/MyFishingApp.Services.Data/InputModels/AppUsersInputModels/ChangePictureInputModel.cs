namespace MyFishingApp.Services.Data.InputModels.AppUsersInputModels
{
    using Microsoft.AspNetCore.Http;

    public class ChangePictureInputModel
    {
        public IFormFile MainImage { get; set; }

        public string UserId { get; set; }
    }
}
