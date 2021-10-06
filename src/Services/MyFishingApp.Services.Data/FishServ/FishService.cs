namespace MyFishingApp.Services.Data.FishServ
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.FishInputModels;

    public class FishService : IFishService
    {
        private readonly IDeletableEntityRepository<Fish> fishRepository;

        public FishService(
            IDeletableEntityRepository<Fish> fishRepository)
        {
            this.fishRepository = fishRepository;
        }

        public async Task CreateAsync(FishInputModel fishInputModel)
        {
            var fishExists = this.fishRepository.All().Where(x => x.Name == fishInputModel.Name).FirstOrDefault();
            if (fishExists is not null)
            {
                throw new Exception("This fish already exists");
            }

            var fish = new Fish()
            {
                Name = fishInputModel.Name,
                Weight = fishInputModel.Weight,
                Lenght = fishInputModel.Lenght,
                Habittat = fishInputModel.Habittat,
                Nutrition = fishInputModel.Nutrition,
                Description = fishInputModel.Description,
                Tips = fishInputModel.Tips,
                ImageUrls = fishInputModel.ImageUrls,
            };

            await this.fishRepository.AddAsync(fish);
            await this.fishRepository.SaveChangesAsync();

            if (fishInputModel.ImageUrls != null)
            {
                Account account = new();

                Cloudinary cloudinary = new(account);
                cloudinary.Api.Secure = true;
                var count = 0;
                foreach (var image in fishInputModel.ImageUrls)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription($"{image.ImageUrl}"),
                        PublicId = fish.Id + count,
                        Folder = "FishApp/FishImages/",
                    };

                    var uploadResult = cloudinary.Upload(uploadParams);
                    count++;
                }

                await this.fishRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteFish(string fishId)
        {
            var fish = this.GetById(fishId);
            if (fish is not null)
            {
                this.fishRepository.Delete(fish);
                await this.fishRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("There is no fish found by this id");
            }
        }

        public IEnumerable<Fish> GetAllFish()
        {
            var fish = this.fishRepository.AllAsNoTracking().Select(x => new Fish
            {
                Name = x.Name,
                Weight = x.Weight,
                Lenght = x.Lenght,
                Habittat = x.Habittat,
                Nutrition = x.Nutrition,
                Description = x.Description,
                Tips = x.Tips,
                ImageUrls = x.ImageUrls,
            }).ToList();

            if (fish.Count > 0)
            {
                return fish;
            }
            else
            {
                throw new Exception("There is no fish found by this id");
            }
        }

        public Fish GetById(string fishId)
        {
            var fish = this.fishRepository.All().Where(x => x.Id == fishId).FirstOrDefault();

            if (fish is not null)
            {
                return fish;
            }
            else
            {
                throw new Exception("There is no fish found by this id");
            }
        }

        public Fish GetByName(string fishName)
        {
            var fish = this.fishRepository.All().Where(x => x.Name == fishName).FirstOrDefault();

            if (fish is not null)
            {
                return fish;
            }
            else
            {
                throw new Exception("There is no fish found by this name!");
            }
        }

        public async Task UpdateFish(FishInputModel fishInputModel, string fishId)
        {
            var fish = this.fishRepository.All().Where(x => x.Id == fishId).FirstOrDefault();

            if (fish is not null)
            {
                fish.Name = fishInputModel.Name;
                fish.Weight = fishInputModel.Weight;
                fish.Lenght = fishInputModel.Lenght;
                fish.Habittat = fishInputModel.Habittat;
                fish.Nutrition = fishInputModel.Nutrition;
                fish.Description = fishInputModel.Description;
                fish.Tips = fishInputModel.Tips;
                fish.ImageUrls = fishInputModel.ImageUrls;

                this.fishRepository.Update(fish);
                await this.fishRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("There is no fish found by this id");
            }
        }
    }
}
