namespace MyFishingApp.Services.Data.InputModels.PostInputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyFishingApp.Data.Models;

    public class CreatePostInputModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<ImageUrls> ImageUrls { get; set; }
    }
}
