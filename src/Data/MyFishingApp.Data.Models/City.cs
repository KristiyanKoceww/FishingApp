namespace MyFishingApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MyFishingApp.Data.Common.Models;

    public class City : BaseDeletableModel<string>
    {
        public City()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual Country Country { get; set; }

        public string CountryName { get; set; }

        public string CountryId { get; set; }
    }
}
