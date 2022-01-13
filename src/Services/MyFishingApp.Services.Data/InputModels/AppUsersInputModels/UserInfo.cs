namespace MyFishingApp.Services.Data.InputModels.AppUsersInputModels
{
    using MyFishingApp.Data.Models;

    public class UserInfo
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public string MainImageUrl { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public int PostsCount { get; set; }

        public int PostsCommentsCount { get; set; }

    }
}
