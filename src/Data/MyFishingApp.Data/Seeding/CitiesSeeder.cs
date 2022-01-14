namespace MyFishingApp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models;
    using Newtonsoft.Json;

    public class CitiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cities.Any())
            {
                return;
            }

            var cities = new List<City>();

            using (StreamReader r = File.OpenText(@"C:\Users\Skyshop\Desktop\Kris\FishingApp\src\Data\MyFishingApp.Data\SeedingData\Cities.json"))
            {
                string json = r.ReadToEnd();
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            for (int i = 0; i < cities.Count; i++)
            {
                dbContext.Cities.Add(cities[i]);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
