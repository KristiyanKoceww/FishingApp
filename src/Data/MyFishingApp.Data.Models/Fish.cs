namespace MyFishingApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyFishingApp.Data.Common.Models;

    public class Fish : BaseDeletableModel<string>
    {
        public Fish()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Images = new HashSet<Image>();
        }

        public string Name { get; set; }

        public double Weight { get; set; }

        public double Lenght { get; set; }

        public string Habittat { get; set; }

        public string Nutrition { get; set; }

        public string Description { get; set; }

        public string Tips { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
