namespace MyFishingApp.Services.Data.FishServ
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.FishInputModels;

    public class FishService : IFishService
    {
        private readonly IDeletableEntityRepository<Fish> fishRepository;
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public FishService(
            IDeletableEntityRepository<Fish> fishRepository,
            IDeletableEntityRepository<Image> imageRepository)
        {
            this.fishRepository = fishRepository;
            this.imageRepository = imageRepository;
        }

        public async Task CreateAsync(FishInputModel fishInputModel)
        {
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

            foreach (var image in fishInputModel.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    Extension = extension,
                    RemoteImageUrl = fishInputModel.ImageUrl,
                };

                fish.Images.Add(dbImage);
                await this.imageRepository.AddAsync(dbImage);
                await this.fishRepository.AddAsync(fish);
                await this.fishRepository.SaveChangesAsync();

                // IMPORT CLOUDINARY TO SAVE THE IMAGES ON CLOUD SERVER
            }
        }

        public async Task DeleteFish(string fishId)
        {
            var fish = this.GetById(fishId);
            this.fishRepository.Delete(fish);
            await this.fishRepository.SaveChangesAsync();
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

            return fish;
        }

        public Fish GetById(string fishId)
        {
            var fish = this.fishRepository.All().Where(x => x.Id == fishId).FirstOrDefault();
            return fish;
        }

        public async Task UpdateFish(FishInputModel fishInputModel, string fishId)
        {
            var fish = this.fishRepository.All().Where(x => x.Id == fishId).FirstOrDefault();
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
    }
}
