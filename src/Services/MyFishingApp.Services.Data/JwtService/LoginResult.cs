namespace MyFishingApp.Services.Data.JwtService
{
    public class LoginResult
    {
        public string UserName { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
