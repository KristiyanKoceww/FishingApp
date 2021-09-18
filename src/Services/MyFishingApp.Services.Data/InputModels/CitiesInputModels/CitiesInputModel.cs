namespace MyFishingApp.Services.Data.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MyFishingApp.Data.Models;

    public class CitiesInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Name must contains only letters")]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Description { get; set; }

        [Required]
        public virtual Country Country { get; set; }
    }
}
