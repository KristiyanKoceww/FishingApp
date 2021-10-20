namespace MyFishingApp.Services.Data.InputModels.PostInputModels
{
    using System.Collections.Generic;

    using MyFishingApp.Data.Models;

    public class UpdatePostInputModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public virtual ICollection<ImageUrls> ImageUrls { get; set; }
    }
}
