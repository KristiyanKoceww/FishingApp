namespace MyFishingApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyFishingApp.Data.Common.Models;

    public class Knot : BaseDeletableModel<string>
    {
        public Knot()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Images = new HashSet<Image>();
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public string VideoUrl { get; set; }
    }
}
