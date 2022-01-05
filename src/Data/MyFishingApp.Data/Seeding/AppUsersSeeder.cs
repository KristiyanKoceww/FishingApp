namespace MyFishingApp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
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

            // pass = adminpassword
            var admin = new ApplicationUser()
            {
                Id = "96f014d7-b8b7-406a-bf1c-52f946a474e0",
                Email = "admin@gmail.com",
                UserName = "admin",
                PasswordHash = "749f09bade8aca755660eeb17792da880218d4fbdc4e25fbec279d7fe9f65d70",
                FirstName = "Admin",
                LastName = "Admin",
                Age = 30,
                Gender = Gender.Male,
            };

            var adminRole = new ApplicationRole()
            {
                Id = "f18bbd11-ac8a-4c31-8fce-a251d5609350",
                Name = "Admin",
            };

            var role = new IdentityUserRole<string>
            {
                RoleId = adminRole.Id,
                UserId = admin.Id,
            };

            admin.Roles.Add(role);
            dbContext.Roles.Add(adminRole);
            dbContext.UserRoles.Add(role);
            dbContext.Users.Add(admin);

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
