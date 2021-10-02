namespace MyFishingApp.Services.Data.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyFishingApp.Data.Models;

    public class CreateReservoirInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Type { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public ICollection<Fish> Fish { get; set; }

        [Required]
        public virtual ICollection<ImageUrls> ImageUrls { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public virtual City City { get; set; }
    }
}
