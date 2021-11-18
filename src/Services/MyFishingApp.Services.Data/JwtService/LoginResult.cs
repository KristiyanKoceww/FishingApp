namespace MyFishingApp.Services.Data.JwtService
{
    public class LoginResult
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
