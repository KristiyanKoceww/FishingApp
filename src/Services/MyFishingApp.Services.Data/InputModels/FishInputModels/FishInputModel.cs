namespace MyFishingApp.Services.Data.InputModels.FishInputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using MyFishingApp.Data.Models;

    public class FishInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public double Weight { get; set; }

        [Required]
        [Range(1, 400)]
        public double Lenght { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Habittat { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Nutrition { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Tips { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public virtual IEnumerable<IFormFile> Images { get; set; }
    }
}
