namespace MyFishingApp.Services.Data.Dam
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

    public class ReservoirService : IReservoirService
    {
        private readonly IDeletableEntityRepository<Reservoir> reservoirRepository;
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public ReservoirService(
            IDeletableEntityRepository<Reservoir> reservoirRepository,
            IDeletableEntityRepository<Image> imageRepository)
        {
            this.reservoirRepository = reservoirRepository;
            this.imageRepository = imageRepository;
        }

        public async Task CreateReservoir(CreateReservoirInputModel createReservoirInputModel)
        {
            var reservoir = new Reservoir()
            {
                Name = createReservoirInputModel.Name,
                Type = createReservoirInputModel.Type,
                Description = createReservoirInputModel.Description,
                City = createReservoirInputModel.City,
                Latitude = createReservoirInputModel.Latitude,
                Longitude = createReservoirInputModel.Longitude,
                Fishs = createReservoirInputModel.Fish,
                ImageUrls = createReservoirInputModel.ImageUrls,
            };

            await this.reservoirRepository.AddAsync(reservoir);
            await this.reservoirRepository.SaveChangesAsync();

            //Account account = new Account();

            //Cloudinary cloudinary = new Cloudinary(account);
            //cloudinary.Api.Secure = true;

            //var uploadParams = new ImageUploadParams()
            //{
            //    File = new FileDescription($"{createReservoirInputModel.ImageUrl}"),
            //    PublicId = reservoir.Id,
            //    Folder = "FishApp/ReservoirImages/",
            //};

            //var uploadResult = cloudinary.Upload(uploadParams);

            //var url = uploadResult.Url.ToString();

            //var imageUrl = new ImageUrls()
            //{
            //    ImageUrl = url,
            //};

            //reservoir.ImageUrls.Add(imageUrl);
            await this.reservoirRepository.SaveChangesAsync();
        }

        public async Task DeleteReservoir(string reservoirId)
        {
            var reservoir = this.GetById(reservoirId);
            this.reservoirRepository.Delete(reservoir);
            await this.reservoirRepository.SaveChangesAsync();
        }

        public IEnumerable<Reservoir> GetAllReservoirs(int page, int itemsPerPage = 12)
        {
            var reservoirs = this.reservoirRepository.AllAsNoTracking().ToList();
            //.OrderByDescending(x => x.Id)
            //.Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
            //.ToList();

            return reservoirs;
        }

        public Reservoir GetById(string reservoirId)
        {
            var reservoir = this.reservoirRepository.All().Where(x => x.Id == reservoirId).FirstOrDefault();

            return reservoir;
        }

        public async Task UpdateReservoir(UpdateReservoirInputModel updateReservoirInputModel, string reservoirId)
        {
            var reservoir = this.GetById(reservoirId);

            if (reservoir is not null)
            {
                reservoir.Name = updateReservoirInputModel.Name;
                reservoir.Type = updateReservoirInputModel.Type;
                reservoir.Description = updateReservoirInputModel.Description;
                reservoir.Latitude = updateReservoirInputModel.Latitude;
                reservoir.Longitude = updateReservoirInputModel.Longitude;
                reservoir.ImageUrls = updateReservoirInputModel.ImageUrls;

                this.reservoirRepository.Update(reservoir);
                await this.reservoirRepository.SaveChangesAsync();
            }
        }
    }
}
