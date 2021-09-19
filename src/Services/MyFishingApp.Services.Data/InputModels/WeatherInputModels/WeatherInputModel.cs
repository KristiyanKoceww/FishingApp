namespace MyFishingApp.Services.Data.InputModels.WeatherInputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WeatherInputModel
    {
        [Required]
        public double Temperature { get; set; }

        public DateTime Date { get; set; }

        public string Main { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public double? Pressure { get; set; }

        [Required]
        public string Wind { get; set; }

        [Required]
        public string Humitity { get; set; }

        [Required]
        public string Cloudy { get; set; }

        [Required]
        public string CityName { get; set; }
    }
}
