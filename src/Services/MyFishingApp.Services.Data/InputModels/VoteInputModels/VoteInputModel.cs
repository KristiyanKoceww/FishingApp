namespace MyFishingApp.Services.Data.InputModels.VoteInputModels
{
    public class VoteInputModel
    {
        public int PostId { get; set; }

        public string UserId { get; set; }

        public bool IsUpVote { get; set; }
    }
}
