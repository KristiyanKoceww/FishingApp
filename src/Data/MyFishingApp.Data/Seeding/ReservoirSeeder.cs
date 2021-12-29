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

    public class ReservoirSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Reservoirs.Any())
            {
                return;
            }

            var reservoirs = new List<Reservoir>();

            using (StreamReader r = File.OpenText(@"C:\Users\Skyshop\source\repos\FishingApp\src\Data\MyFishingApp.Data\SeedingData\Reservoirs.json"))
            {
                string json = r.ReadToEnd();
                reservoirs = JsonConvert.DeserializeObject<List<Reservoir>>(json);
            }

            for (int i = 0; i < reservoirs.Count; i++)
            {
                dbContext.Reservoirs.Add(reservoirs[i]);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
