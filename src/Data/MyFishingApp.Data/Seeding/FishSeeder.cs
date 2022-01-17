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

    public class FishSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Fish.Any())
            {
                return;
            }

            var fish = new List<Fish>();

            using (StreamReader r = File.OpenText(@"C:\Users\User\Desktop\FishingApp\src\Data\MyFishingApp.Data\SeedingData\Fish.json"))
            {
                string json = r.ReadToEnd();
                fish = JsonConvert.DeserializeObject<List<Fish>>(json);
            }

            for (int i = 0; i < fish.Count; i++)
            {
                dbContext.Fish.Add(fish[i]);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
