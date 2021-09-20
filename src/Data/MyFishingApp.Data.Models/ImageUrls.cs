namespace MyFishingApp.Data.Models
{
    using System;

    using MyFishingApp.Data.Common.Models;

    public class ImageUrls : BaseDeletableModel<string>
    {
        public ImageUrls()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ImageUrl { get; set; }
    }
}
