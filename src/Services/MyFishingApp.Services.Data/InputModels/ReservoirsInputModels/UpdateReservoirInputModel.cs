﻿namespace MyFishingApp.Services.Data.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyFishingApp.Data.Models;

    public class UpdateReservoirInputModel
    {
        [Required]
        public string ReservoirId { get; set; }

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
        [Range(2, 30)]
        public double Latitude { get; set; }

        [Required]
        [Range(2, 30)]
        public double Longitude { get; set; }

        public IFormFileCollection FormFiles { get; set; }
    }
}
