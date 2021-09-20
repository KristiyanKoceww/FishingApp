namespace MyFishingApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MyFishingApp.Data.Common.Models;

    public class Reservoir : BaseDeletableModel<string>
    {
        public Reservoir()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Fishs = new HashSet<Fish>();
            this.Images = new HashSet<Image>();
            this.ImageUrls = new HashSet<ImageUrls>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public virtual Weather1 Weather { get; set; }

        [Required]
        public virtual City City { get; set; }

        public virtual ICollection<Fish> Fishs { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ImageUrls> ImageUrls { get; set; }
    }
}
