namespace MyFishingApp.Services.Data.NEWJWTSERVICE
{
    using MyFishingApp.Data.Models;

    public class SignInResult
    {
        public SignInResult()
        {
            this.Success = false;
        }

        public bool Success { get; set; }

        public ApplicationUser User { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
