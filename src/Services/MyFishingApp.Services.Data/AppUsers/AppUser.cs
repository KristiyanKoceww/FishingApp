﻿namespace MyFishingApp.Services.Data.AppUsers
{
    using System;
    using System.IO;
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
        private readonly IRepository<IdentityUserClaim<string>> claimRepository;

        public AppUser(
            IRepository<ApplicationUser> appUserRepository,
            IRepository<IdentityUserClaim<string>> claimRepository)
        {
            this.appUserRepository = appUserRepository;
            this.claimRepository = claimRepository;
        }

        public static Cloudinary Cloudinary()
        {
            Account account = new();
            Cloudinary cloudinary = new(account);
            cloudinary.Api.Secure = true;

            return cloudinary;
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

        private void BuildClaims(ApplicationUser user)
        {
            var claim = new IdentityUserClaim<string>
            {
                UserId = user.Id,
                ClaimType = "NameIdentifier",
                ClaimValue = user.UserName,
            };

            user.Claims.Add(claim);
            this.claimRepository.AddAsync(claim);

            this.appUserRepository.SaveChangesAsync();
            this.claimRepository.SaveChangesAsync();
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
                LastName = userInputModel.LastName,
                Email = userInputModel.Email,
                UserName = userInputModel.UserName,
                PasswordHash = ComputeSha256Hash(userInputModel.Password),
                Age = userInputModel.Age,
                Gender = userInputModel.Gender,
            };

            if (userInputModel.MiddleName != null)
            {
                user.MiddleName = userInputModel.MiddleName;
            }
            else if (userInputModel.PhoneNumber != null)
            {
                user.PhoneNumber = userInputModel.PhoneNumber;
            }

            if (userInputModel.MainImage != null)
            {
                var cloudinary = Cloudinary();
                byte[] bytes;
                using (var memoryStream = new MemoryStream())
                {
                    userInputModel.MainImage.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                string base64 = Convert.ToBase64String(bytes);

                var prefix = @"data:image/png;base64,";
                var imagePath = prefix + base64;

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imagePath),
                    Folder = "FishApp/UserImages/",
                };

                var uploadResult = await cloudinary.UploadAsync(@uploadParams);

                var error = uploadResult.Error;

                if (error != null)
                {
                    throw new Exception($"Error: {error.Message}");
                }

                user.MainImageUrl = uploadResult.SecureUrl.AbsoluteUri;
            }
            else
            {
                user.MainImageUrl = "https://res.cloudinary.com/kocewwcloud/image/upload/v1639749727/FishApp/DefaultProfilePicture/Default_profile_picture_cyg6fr.png";
            }

            this.BuildClaims(user);

            await this.appUserRepository.AddAsync(user);
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

        public async Task ChangeUserProfilePicture(ChangePictureInputModel changePictureInputModel)
        {
            var user = this.GetById(changePictureInputModel.UserId);

            if (changePictureInputModel.MainImage != null)
            {
                var cloudinary = Cloudinary();
                byte[] bytes;
                using (var memoryStream = new MemoryStream())
                {
                    changePictureInputModel.MainImage.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                string base64 = Convert.ToBase64String(bytes);

                var prefix = @"data:image/png;base64,";
                var imagePath = prefix + base64;

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imagePath),
                    Folder = "FishApp/UserImages/",
                };

                var uploadResult = await cloudinary.UploadAsync(@uploadParams);

                var error = uploadResult.Error;

                if (error != null)
                {
                    throw new Exception($"Error: {error.Message}");
                }

                user.MainImageUrl = uploadResult.SecureUrl.AbsoluteUri;
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
            var user = this.appUserRepository.All().Where(x => x.UserName == username).Select(x => new ApplicationUser
            {
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.MiddleName,
                Age = x.Age,
                Id = x.Id,
                Email = x.Email,
                Gender = x.Gender,
                MainImageUrl = x.MainImageUrl,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName,
                Posts = x.Posts.Select(x => new Post
                {
                    Id = x.Id,
                    ImageUrls = x.ImageUrls,
                    Title = x.Title,
                    User = x.User,
                    UserId = x.UserId,
                    Content = x.Content,
                    Votes = x.Votes,
                    Comments = x.Comments.Select(x => new Comment
                    {
                        Content = x.Content,
                        User = x.User,
                        UserId = x.UserId,
                        Id = x.Id,
                        Parent = x.Parent,
                        ParentId = x.ParentId,
                        Post = x.Post,
                        PostId = x.PostId,
                    }).ToList(),
                }).ToList(),
            }).FirstOrDefault();

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
