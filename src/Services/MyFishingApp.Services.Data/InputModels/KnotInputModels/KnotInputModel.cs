namespace MyFishingApp.Services.Data.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyFishingApp.Data.Models;

    public class KnotInputModel
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
        public virtual ICollection<ImageUrls> ImageUrls { get; set; }

        public string VideoUrl { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
