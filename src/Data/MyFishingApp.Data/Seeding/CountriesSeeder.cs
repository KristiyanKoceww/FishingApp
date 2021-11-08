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

    public class CountriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cities.Any())
            {
                return;
            }

            var countries = new List<Country>();

            using (StreamReader r = File.OpenText(@"C:\Users\User\Desktop\FishingApp\src\Data\MyFishingApp.Data\SeedingData\Countries.json"))
            {
                string json = r.ReadToEnd();
                countries = JsonConvert.DeserializeObject<List<Country>>(json);
            }

            for (int i = 0; i < countries.Count; i++)
            {
                dbContext.Countries.Add(countries[i]);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
