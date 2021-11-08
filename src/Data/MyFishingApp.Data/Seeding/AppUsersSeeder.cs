namespace MyFishingApp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models;
    using Newtonsoft.Json;

    public class AppUsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var users = new List<ApplicationUser>();

            using (StreamReader r = File.OpenText(@"C:\Users\User\Desktop\FishingApp\src\Data\MyFishingApp.Data\SeedingData\AppUsers.json"))
            {
                string json = r.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<ApplicationUser>>(json);
            }

            for (int i = 0; i < users.Count; i++)
            {
                dbContext.Users.Add(users[i]);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
