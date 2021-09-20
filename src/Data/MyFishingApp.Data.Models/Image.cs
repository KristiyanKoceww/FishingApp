﻿namespace MyFishingApp.Data.Models
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

        public string ReservoirId { get; set; }

        public virtual Reservoir Reservoir { get; set; }

        public string FishUserId { get; set; }

        public virtual FishUser FishUser { get; set; }
    }
}
