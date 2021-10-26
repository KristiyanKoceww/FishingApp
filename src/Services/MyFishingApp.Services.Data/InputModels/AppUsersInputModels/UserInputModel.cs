namespace MyFishingApp.Services.Data.InputModels.AppUsersInputModels
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyFishingApp.Data.Models;

    public class UserInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Name must contains only letters")]
        public string FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(30)]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Name must contains only letters")]
        public string MiddleName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Name must contains only letters")]
        public string LastName { get; set; }

        [Required]
        [Range(10, 120)]
        public int Age { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        public IFormFile MainImage { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}
