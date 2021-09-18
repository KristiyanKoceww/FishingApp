using System.ComponentModel.DataAnnotations;

namespace MyFishingApp.Services.Data.InputModels
{
    public class UpdateReservoirInputModel
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
        [MinLength(3)]
        [MaxLength(20)]
        public double Latitude { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public double Longitude { get; set; }
    }
}
