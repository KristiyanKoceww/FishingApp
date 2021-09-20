namespace MyFishingApp.Data.Models
{
    using System;

    using MyFishingApp.Data.Common.Models;

    public class Weather1 : BaseDeletableModel<string>
    {
        public Weather1()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public double Temperature { get; set; }

        public DateTime Date { get; set; }

        public string Main { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double? Pressure { get; set; }

        public string Wind { get; set; }

        public string Humitity { get; set; }

        public string Cloudy { get; set; }

        public virtual City City { get; set; }

        public string CityName { get; set; }
    }
}
