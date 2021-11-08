namespace MyFishingApp.Services.Data.InputModels.CommentsInputModels
{
   public class CommentsInputModel
    {
        public int PostId { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public int? ParentId { get; set; }
    }
}
