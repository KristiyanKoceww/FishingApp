namespace MyFishingApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyFishingApp.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }

        public string FishId { get; set; }

        public virtual Fish Fish { get; set; }

        public string DamId { get; set; }

        public virtual Reservoir Dam { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
