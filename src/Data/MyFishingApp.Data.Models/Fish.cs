namespace MyFishingApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyFishingApp.Data.Common.Models;

    public class Fish : BaseDeletableModel<string>
    {
        public Fish()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ImageUrls = new HashSet<ImageUrls>();
        }

        [Required]
        public string Name { get; set; }

        public double Weight { get; set; }

        public double Lenght { get; set; }

        [Required]
        public string Habittat { get; set; }

        [Required]
        public string Nutrition { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Tips { get; set; }

        public virtual ICollection<ImageUrls> ImageUrls { get; set; }
    }
}
