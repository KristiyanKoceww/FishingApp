namespace MyFishingApp.Services.Data.AppUsers
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Identity;
    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.AppUsersInputModels;

    public class AppUser : IAppUser
    {
        private readonly IRepository<ApplicationUser> appUserRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public AppUser(
            IRepository<ApplicationUser> appUserRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.appUserRepository = appUserRepository;
            this.userManager = userManager;
        }

        public static string ComputeSha256Hash(string password)
        {
            // Create a SHA256
            using SHA256 sha256Hash = SHA256.Create();

            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert byte array to a string
            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public async Task CreateAsync(UserInputModel userInputModel)
        {
            var userName = this.appUserRepository.All().Where(x => x.UserName == userInputModel.UserName).FirstOrDefault();
            if (userName != null)
            {
                throw new Exception("Username already taken by another user.Please enter new username.");
            }

            if (string.IsNullOrWhiteSpace(userInputModel.Password) || string.IsNullOrWhiteSpace(userInputModel.UserName))
            {
                throw new Exception("Password and username are required");
            }

            var user = new ApplicationUser()
            {
                FirstName = userInputModel.FirstName,
                MiddleName = userInputModel.MiddleName,
                LastName = userInputModel.LastName,
                Email = userInputModel.Email,
                PhoneNumber = userInputModel.PhoneNumber,
                UserName = userInputModel.UserName,
                PasswordHash = ComputeSha256Hash(userInputModel.Password),
                Age = userInputModel.Age,
                Gender = userInputModel.Gender,
            };

            await this.appUserRepository.AddAsync(user);
            await this.appUserRepository.SaveChangesAsync();

            if (userInputModel.MainImageUrl != null)
            {
                Account account = new();
                Cloudinary cloudinary = new(account);
                cloudinary.Api.Secure = true;

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription($"{userInputModel.MainImageUrl}"),
                    PublicId = user.Id,
                    Folder = "FishApp/UserImages/",
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                var url = uploadResult.Url.ToString();

                var imageUrl = new ImageUrls()
                {
                    ImageUrl = url,
                };

                user.MainImageUrl = url;
                await this.appUserRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(string userId)
        {
            var user = this.GetById(userId);
            if (user is not null)
            {
                this.appUserRepository.Delete(user);
                await this.appUserRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No user found  by this id");
            }
        }

        public ApplicationUser GetById(string userId)
        {
            var user = this.appUserRepository.All().Where(x => x.Id == userId).FirstOrDefault();

            if (user is not null)
            {
                return user;
            }
            else
            {
                throw new Exception("No user found  by this id");
            }
        }

        public ApplicationUser Authenticate(string username, string password)
        {
            var dbUser = this.appUserRepository.All().Where(x => x.UserName == username).FirstOrDefault();

            if (dbUser == null)
            {
                return null;
            }

            var userPassHash = ComputeSha256Hash(password);

            if (!(dbUser.UserName == username && dbUser.PasswordHash == userPassHash))
            {
                throw new Exception("Invalid username or password!");
            }

            return dbUser;
        }

        public async Task UpdateUserAsync(UserInputModel userInputModel, string userId)
        {
            var user = this.GetById(userId);

            if (user is not null)
            {
                user.FirstName = userInputModel.FirstName;
                user.MiddleName = userInputModel.MiddleName;
                user.LastName = userInputModel.LastName;
                user.Email = userInputModel.Email;
                user.PhoneNumber = userInputModel.PhoneNumber;
                user.UserName = userInputModel.UserName;
                user.PasswordHash = ComputeSha256Hash(userInputModel.Password);
                user.Age = userInputModel.Age;
                user.Gender = userInputModel.Gender;

                this.appUserRepository.Update(user);
                await this.appUserRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No user found  by this id");
            }
        }

        public ApplicationUser GetByUsername(string username)
        {
            var user = this.appUserRepository.All().Where(x => x.UserName == username).FirstOrDefault();

            if (user is not null)
            {
                return user;
            }
            else
            {
                throw new Exception("No user found  by this username");
            }
        }
    }
}
