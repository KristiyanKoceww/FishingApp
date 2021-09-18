namespace MyFishingApp.Services.Data.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class CountryInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Name must contains only letters")]
        public string Name { get; set; }
    }
}
