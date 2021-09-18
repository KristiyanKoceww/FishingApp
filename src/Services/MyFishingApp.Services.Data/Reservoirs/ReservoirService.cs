namespace MyFishingApp.Services.Data.Dam
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

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
            };

            foreach (var image in createReservoirInputModel.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    Reservoir = reservoir,
                    ReservoirId = reservoir.Id,
                    Extension = extension,
                    RemoteImageUrl = createReservoirInputModel.ImageUrl,
                };

                reservoir.Images.Add(dbImage);
                await this.imageRepository.AddAsync(dbImage);
                await this.reservoirRepository.AddAsync(reservoir);
                await this.reservoirRepository.SaveChangesAsync();

                // IMPORT CLOUDINARY TO SAVE THE IMAGES ON CLOUD SERVER
            }
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

            reservoir.Name = updateReservoirInputModel.Name;
            reservoir.Type = updateReservoirInputModel.Type;
            reservoir.Description = updateReservoirInputModel.Description;
            reservoir.Latitude = updateReservoirInputModel.Latitude;
            reservoir.Longitude = updateReservoirInputModel.Longitude;

            this.reservoirRepository.Update(reservoir);
            await this.reservoirRepository.SaveChangesAsync();
        }
    }
}
