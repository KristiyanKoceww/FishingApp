﻿namespace MyFishingApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyFishingApp.Data.Common.Models;

    public class Knot : BaseDeletableModel<string>
    {
        public Knot()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ImageUrls = new HashSet<ImageUrls>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public virtual ICollection<ImageUrls> ImageUrls { get; set; }
    }
}
