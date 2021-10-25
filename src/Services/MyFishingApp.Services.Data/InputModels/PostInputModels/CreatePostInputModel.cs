namespace MyFishingApp.Services.Data.InputModels.PostInputModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreatePostInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
