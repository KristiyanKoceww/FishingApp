namespace MyFishingApp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models;
    using Newtonsoft.Json;

    public class KnotsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Knots.Any())
            {
                return;
            }

            var knots = new List<Knot>();

            using (StreamReader r = File.OpenText(@"C:\Users\User\Desktop\FishingApp\src\Data\MyFishingApp.Data\SeedingData\Knots.json"))
            {
                string json = r.ReadToEnd();
                knots = JsonConvert.DeserializeObject<List<Knot>>(json);
            }

            for (int i = 0; i < knots.Count; i++)
            {
                dbContext.Knots.Add(knots[i]);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
