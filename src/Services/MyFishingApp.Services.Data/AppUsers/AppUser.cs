namespace MyFishingApp.Services.Data.AppUsers
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.AppUsersInputModels;

    public class AppUser : IAppUser
    {
        private readonly IRepository<ApplicationUser> appUserRepository;

        public AppUser(IRepository<ApplicationUser> appUserRepository)
        {
            this.appUserRepository = appUserRepository;
        }

        public static string ComputeSha256Hash(string password)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public async Task CreateAsync(UserInputModel userInputModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = userInputModel.FirstName,
                MiddleName = userInputModel.MiddleName,
                LastName = userInputModel.LastName,
                Email = userInputModel.Email,
                Phone = userInputModel.Phone,
                UserName = userInputModel.UserName,
                PasswordHash = ComputeSha256Hash(userInputModel.Password),
                Age = userInputModel.Age,
                Gender = userInputModel.Gender,
            };

            await this.appUserRepository.AddAsync(user);
            await this.appUserRepository.SaveChangesAsync();

            Account account = new Account();
            Cloudinary cloudinary = new Cloudinary(account);
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

        public async Task UpdateUserAsync(UserInputModel userInputModel, string userId)
        {
            var user = this.GetById(userId);

            if (user is not null)
            {
                user.FirstName = userInputModel.FirstName;
                user.MiddleName = userInputModel.MiddleName;
                user.LastName = userInputModel.LastName;
                user.Email = userInputModel.Email;
                user.Phone = userInputModel.Phone;
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
    }
}
