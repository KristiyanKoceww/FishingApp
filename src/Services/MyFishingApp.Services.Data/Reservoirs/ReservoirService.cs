namespace MyFishingApp.Services.Data.Dam

{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

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

        public void CreateReservoir(CreateReservoirInputModel createReservoirInputModel)
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
                this.imageRepository.AddAsync(dbImage);

                // IMPORT CLOUDINARY TO SAVE THE IMAGES ON CLOUD SERVER
            }
        }

        public void DeleteReservoir(string reservoirId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Reservoir> GetAllDams()
        {
            //var dams = this.reservoirRepository.All().Select(x => new Reservoir
            //{
            //    x.Name,
            //    x.Type,
            //    x.Description,
            //    x.Latitude,
            //    x.Longitude,
            //}).ToArray();

            var dam = new Reservoir()
            {
                Name = "Dqkovo",
                Type = "golqm",
                Description = "Dqkovo",
                Latitude = 13513513,
                Longitude = 1535325,
            };

            var dam2 = new Reservoir()
            {
                Name = "Dqkovo2222",
                Type = "golqm",
                Description = "Dqkovo",
                Latitude = 135525353513,
                Longitude = 15355235235325,
            };

            var dam3 = new Reservoir()
            {
                Name = "Oslome",
                Type = "Edar",
                Description = "Oslome is the best",
                Latitude = 123,
                Longitude = 321,
            };

            var list = new List<Reservoir>();
            list.Add(dam);
            list.Add(dam2);
            list.Add(dam3);

            return list;
        }

        public void UpdateReservoir(string reservoirId)
        {
            throw new System.NotImplementedException();
        }
    }
}
