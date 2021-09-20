namespace MyFishingApp.Services.Data.Knots
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task CreateKnot(KnotInputModel knotInputModel)
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

            //foreach (var image in knotInputModel.Images)
            //{
            //    var extension = Path.GetExtension(image.FileName).TrimStart('.');
            //    if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            //    {
            //        throw new Exception($"Invalid image extension {extension}");
            //    }

            //    var dbImage = new Image
            //    {

            //        Extension = extension,
            //        RemoteImageUrl = knotInputModel.ImageUrl,
            //    };

            //    // IMPORT CLOUDINARY TO SAVE THE IMAGES ON CLOUD SERVER
            //}

            await this.knotRepository.AddAsync(knot);
            await this.knotRepository.SaveChangesAsync();
        }

        public async Task DeleteKnot(string knotId)
        {
            var knot = this.GetById(knotId);
            if (knot != null)
            {
                this.knotRepository.Delete(knot);
                await this.knotRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<Knot> GetAllKnots(int page, int itemsPerPage = 12)
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

        public async Task UpdateKnot(KnotInputModel knotInputModel, string knotId)
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
