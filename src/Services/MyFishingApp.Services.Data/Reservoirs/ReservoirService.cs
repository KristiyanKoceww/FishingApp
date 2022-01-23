namespace MyFishingApp.Services.Data.Dam
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
        private readonly IDeletableEntityRepository<City> citiesRepository;

        public ReservoirService(
            IDeletableEntityRepository<Reservoir> reservoirRepository,
            IDeletableEntityRepository<City> citiesRepository)
        {
            this.reservoirRepository = reservoirRepository;
            this.citiesRepository = citiesRepository;
        }

        public static Cloudinary Cloudinary()
        {
            Account account = new();
            Cloudinary cloudinary = new(account);
            cloudinary.Api.Secure = true;

            return cloudinary;
        }

        public async Task CreateReservoir(CreateReservoirInputModel createReservoirInputModel)
        {
            var reservoirExists = this.reservoirRepository.All()
                .Where(x => x.Name == createReservoirInputModel.Name)
                .FirstOrDefault();
            if (reservoirExists is not null)
            {
                throw new Exception("This reservoir already exists");
            }

            var city = this.citiesRepository.All().Where(x => x.Id == createReservoirInputModel.CityId).FirstOrDefault();

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = ",";
            var latitude = Convert.ToDouble(createReservoirInputModel.Latitude, provider);
            var longitude = Convert.ToDouble(createReservoirInputModel.Longitude, provider);

            var reservoir = new Reservoir()
            {
                Name = createReservoirInputModel.Name,
                Type = createReservoirInputModel.Type,
                Description = createReservoirInputModel.Description,
                Latitude = latitude,
                Longitude = longitude,
            };

            if (createReservoirInputModel.Fish != null)
            {
                foreach (var fish in createReservoirInputModel.Fish)
                {
                    reservoir.Fish.Add(fish);
                }
            }

            if (city is not null)
            {
                reservoir.City = city;
                reservoir.CityId = city.Id;
            }

            if (createReservoirInputModel.Images != null)
            {
                var cloudinary = Cloudinary();
                foreach (var image in createReservoirInputModel.Images)
                {
                    byte[] bytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        image.CopyTo(memoryStream);
                        bytes = memoryStream.ToArray();
                    }

                    string base64 = Convert.ToBase64String(bytes);

                    var prefix = @"data:image/png;base64,";
                    var imagePath = prefix + base64;

                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(imagePath),
                        Folder = "FishApp/ReservoirImages/",
                    };

                    var uploadResult = await cloudinary.UploadAsync(@uploadParams);

                    var error = uploadResult.Error;

                    if (error != null)
                    {
                        throw new Exception($"Error: {error.Message}");
                    }

                    var imageUrl = new ImageUrls()
                    {
                        ImageUrl = uploadResult.SecureUrl.AbsoluteUri,
                    };

                    reservoir.ImageUrls.Add(imageUrl);
                }
            }
            await this.reservoirRepository.AddAsync(reservoir);
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

        public IEnumerable<Reservoir> GetAllReservoirs()
        {
            var reservoirs = this.reservoirRepository
                .All()
                .Select(x => new Reservoir()
                {
                    Name = x.Name,
                    Type = x.Type,
                    Description = x.Description,
                    ImageUrls = x.ImageUrls,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    //City = x.City.,
                    Fish = x.Fish,
                    Id = x.Id,
                    CityId = x.CityId,
                }).ToList();

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

        public Reservoir GetByName(string reservoirName)
        {
            var res = this.reservoirRepository.All()
                .Where(x => x.Name == reservoirName).FirstOrDefault();

            var reservoir = this.reservoirRepository.All()
                .Where(x => x.Name == reservoirName)
                .Select(x => new Reservoir
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    Description = x.Description,
                    ImageUrls = x.ImageUrls,
                    Longitude = x.Longitude,
                    Latitude = x.Latitude,
                    //City = x.City,
                    CityId = x.CityId,
                    Fish = x.Fish,
                })
                .FirstOrDefault();

            if (reservoir is not null)
            {
                return reservoir;
            }
            else
            {
                throw new Exception("No reservoir found by this name!");
            }
        }

        public IEnumerable<Reservoir> GetRandomFourReservoirs()
        {
            var reservoirs = this.reservoirRepository
                .AllAsNoTracking()
                .Select(x => new Reservoir
                {
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrls = x.ImageUrls,
                }).OrderBy(arg => Guid.NewGuid()).Take(4).ToList();

            if (reservoirs.Count > 0)
            {
                return reservoirs;
            }
            else
            {
                throw new Exception("No reservoirs found");
            }
        }

        public async Task UpdateReservoir(UpdateReservoirInputModel updateReservoirInputModel)
        {
            var reservoir = this.reservoirRepository.All().Where(x => x.Id == updateReservoirInputModel.ReservoirId).FirstOrDefault();
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = ",";
            var latitude = Convert.ToDouble(updateReservoirInputModel.Latitude, provider);
            var longitude = Convert.ToDouble(updateReservoirInputModel.Longitude, provider);
            if (reservoir is not null)
            {
                reservoir.Name = updateReservoirInputModel.Name;
                reservoir.Type = updateReservoirInputModel.Type;
                reservoir.Description = updateReservoirInputModel.Description;
                reservoir.Latitude = latitude;
                reservoir.Longitude = longitude;

                if (updateReservoirInputModel.FormFiles != null)
                {
                    var cloudinary = Cloudinary();
                    foreach (var image in updateReservoirInputModel.FormFiles)
                    {
                        byte[] bytes;
                        using (var memoryStream = new MemoryStream())
                        {
                            image.CopyTo(memoryStream);
                            bytes = memoryStream.ToArray();
                        }

                        string base64 = Convert.ToBase64String(bytes);

                        var prefix = @"data:image/png;base64,";
                        var imagePath = prefix + base64;

                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(imagePath),
                            Folder = "FishApp/ReservoirImages/",
                        };

                        var uploadResult = await cloudinary.UploadAsync(@uploadParams);

                        var error = uploadResult.Error;

                        if (error != null)
                        {
                            throw new Exception($"Error: {error.Message}");
                        }

                        var imageUrl = new ImageUrls()
                        {
                            ImageUrl = uploadResult.SecureUrl.AbsoluteUri,
                        };

                        reservoir.ImageUrls.Add(imageUrl);
                    }
                }

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
