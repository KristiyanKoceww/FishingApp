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
        private readonly IDeletableEntityRepository<Image> imageRepository;

        public FishService(
            IDeletableEntityRepository<Fish> fishRepository,
            IDeletableEntityRepository<Image> imageRepository)
        {
            this.fishRepository = fishRepository;
            this.imageRepository = imageRepository;
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

            //Account account = new Account();

            //Cloudinary cloudinary = new Cloudinary(account);
            //cloudinary.Api.Secure = true;

            //var uploadParams = new ImageUploadParams()
            //{
            //    File = new FileDescription($"{fishInputModel.ImageUrl}"),
            //    PublicId = fish.Id,
            //    Folder = "FishApp/FishImages/",
            //};

            //var uploadResult = cloudinary.Upload(uploadParams);

            //var url = uploadResult.Url.ToString();

            //var imageUrl = new ImageUrls()
            //{
            //    ImageUrl = url,
            //};

            //fish.ImageUrls.Add(imageUrl);
            //await this.fishRepository.SaveChangesAsync();
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
                Images = x.Images,
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
