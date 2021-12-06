namespace MyFishingApp.Services.Data.JwtService
{
    using MyFishingApp.Data.Models;

    public class LoginResult
    {
        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
