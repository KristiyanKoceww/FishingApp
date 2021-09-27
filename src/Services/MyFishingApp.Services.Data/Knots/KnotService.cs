namespace MyFishingApp.Services.Data.Knots
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;

    public class KnotService : IKnotService
    {
        private readonly IDeletableEntityRepository<Knot> knotRepository;
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public KnotService(
            IDeletableEntityRepository<Knot> knotRepository,
            IDeletableEntityRepository<Image> imageRepository)
        {
            this.knotRepository = knotRepository;
            this.imageRepository = imageRepository;
        }

        public async Task CreateKnotAsync(KnotInputModel knotInputModel)
        {
            var knot = new Knot()
            {
                Name = knotInputModel.Name,
                Type = knotInputModel.Type,
                Description = knotInputModel.Description,
                ImageUrls = knotInputModel.ImageUrls,
            };

            if (knotInputModel.VideoUrl is not null)
            {
                knot.VideoUrl = knotInputModel.VideoUrl;
            }

            await this.knotRepository.AddAsync(knot);
            await this.knotRepository.SaveChangesAsync();

            //Account account = new Account();

            //Cloudinary cloudinary = new Cloudinary(account);
            //cloudinary.Api.Secure = true;

            //var uploadParams = new ImageUploadParams()
            //{
            //    File = new FileDescription($"{knotInputModel.ImageUrl}"),
            //    PublicId = knot.Id,
            //    Folder = "FishApp/KnotImages/",
            //};

            //var uploadResult = cloudinary.Upload(uploadParams);

            //var url = uploadResult.Url.ToString();

            //var imageUrl = new ImageUrls()
            //{
            //    ImageUrl = url,
            //};

            //knot.ImageUrls.Add(imageUrl);
            //await this.knotRepository.SaveChangesAsync();
        }

        public async Task DeleteKnotAsync(string knotId)
        {
            var knot = this.GetById(knotId);
            if (knot != null)
            {
                this.knotRepository.Delete(knot);
                await this.knotRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<Knot> GetAllKnots()
        {
            var knots = this.knotRepository.AllAsNoTracking().Select(x => new Knot
            {
                Name = x.Name,
                Type = x.Type,
                Description = x.Description,
                Images = x.Images,
                ImageUrls = x.ImageUrls,
            }).ToList();

            return knots;
        }

        public Knot GetById(string knotId)
        {
            var knot = this.knotRepository.All().Where(x => x.Id == knotId).FirstOrDefault();
            return knot;
        }

        public async Task UpdateKnotAsync(KnotInputModel knotInputModel, string knotId)
        {
            // Update images and Images URL
            var knot = this.GetById(knotId);
            if (knot != null)
            {
                knot.Name = knotInputModel.Name;
                knot.Type = knotInputModel.Type;
                knot.Description = knotInputModel.Description;
                knot.ImageUrls = knotInputModel.ImageUrls;

                this.knotRepository.Update(knot);
                await this.knotRepository.SaveChangesAsync();
            }
        }
    }
}
