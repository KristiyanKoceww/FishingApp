namespace MyFishingApp.Services.Data.InputModels.KnotInputModels
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class UpdateKnotInputModel
    {
        [Required]
        public string KnotId { get; set; }

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

        public virtual IFormFileCollection Images { get; set; }

        public string VideoUrl { get; set; }
    }
}
