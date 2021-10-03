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

        public ReservoirService(
            IDeletableEntityRepository<Reservoir> reservoirRepository)
        {
            this.reservoirRepository = reservoirRepository;
        }

        public async Task CreateReservoir(CreateReservoirInputModel createReservoirInputModel)
        {
            var reservoirExists = this.reservoirRepository.All().Where(x => x.Name == createReservoirInputModel.Name).FirstOrDefault();
            if (reservoirExists is not null)
            {
                throw new Exception("This reservoir already exists");
            }

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

            Account account = new();
            Cloudinary cloudinary = new(account);
            cloudinary.Api.Secure = true;
            var count = 0;
            foreach (var image in createReservoirInputModel.ImageUrls)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription($"{image.ImageUrl}"),
                    PublicId = reservoir.Id + count,
                    Folder = "FishApp/ReservoirImages/",
                };

                var uploadResult = cloudinary.Upload(uploadParams);
                count++;
            }

            // var url = uploadResult.Url.ToString();

            // var imageUrl = new ImageUrls()
            // {
            //    ImageUrl = url,
            // };

            // reservoir.ImageUrls.Add(imageUrl);
            await this.reservoirRepository.SaveChangesAsync();
        }

        public async Task DeleteReservoir(string reservoirId)
        {
            var reservoir = this.GetById(reservoirId);
            if (reservoir is not null)
            {
                this.reservoirRepository.Delete(reservoir);
                await this.reservoirRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No reservoir found by this id");
            }
        }

        public IEnumerable<Reservoir> GetAllReservoirs(int page, int itemsPerPage = 12)
        {
            var reservoirs = this.reservoirRepository.AllAsNoTracking().ToList();

             // .OrderByDescending(x => x.Id)
            // .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
            // .ToList();
            if (reservoirs.Count > 0)
            {
                return reservoirs;
            }
            else
            {
                throw new Exception("No reservoirs found");
            }
        }

        public Reservoir GetById(string reservoirId)
        {
            var reservoir = this.reservoirRepository.All().Where(x => x.Id == reservoirId).FirstOrDefault();
            if (reservoir is not null)
            {
                return reservoir;
            }
            else
            {
                throw new Exception("No reservoir found by this id");
            }
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
            else
            {
                throw new Exception("No reservoir found by this id");
            }
        }
    }
}
