namespace MyFishingApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyFishingApp.Data.Common.Models;

    public class Reservoir : BaseDeletableModel<string>
    {
        public Reservoir()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Fishs = new HashSet<Fish>();
            this.Images = new HashSet<Image>();
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public virtual ICollection<Fish> Fishs { get; set; }

        public virtual Weather Weather { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
