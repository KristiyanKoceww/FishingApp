namespace MyFishingApp.Services.Data.InputModels.AppUsersInputModels
{
    using MyFishingApp.Data.Models;

    public class UpdateUserResult
    {
        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
