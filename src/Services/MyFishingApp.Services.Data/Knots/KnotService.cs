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
    using MyFishingApp.Services.Data.InputModels.KnotInputModels;

    public class KnotService : IKnotService
    {
        private readonly IDeletableEntityRepository<Knot> knotRepository;

        public KnotService(
            IDeletableEntityRepository<Knot> knotRepository)
        {
            this.knotRepository = knotRepository;
        }

        public static Cloudinary Cloudinary()
        {
            Account account = new();
            Cloudinary cloudinary = new(account);
            cloudinary.Api.Secure = true;

            return cloudinary;
        }

        public async Task CreateKnotAsync(KnotInputModel knotInputModel)
        {
            var knotExists = this.knotRepository.All().Where(x => x.Name == knotInputModel.Name).FirstOrDefault();
            if (knotExists is not null)
            {
                throw new Exception("This knot already exists");
            }

            var knot = new Knot()
            {
                Name = knotInputModel.Name,
                Type = knotInputModel.Type,
                Description = knotInputModel.Description,
            };

            if (knotInputModel.VideoUrl is not null)
            {
                knot.VideoUrl = knotInputModel.VideoUrl;
            }

            if (knotInputModel.Images.Count > 0)
            {
                var cloudinary = Cloudinary();

                foreach (var image in knotInputModel.Images)
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
                        Folder = "FishApp/KnotImages/",
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

                    knot.ImageUrls.Add(imageUrl);
                }
            }

            await this.knotRepository.AddAsync(knot);
            await this.knotRepository.SaveChangesAsync();
        }

        public async Task DeleteKnotAsync(string knotId)
        {
            var knot = this.GetById(knotId);
            if (knot is not null)
            {
                this.knotRepository.Delete(knot);
                await this.knotRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No knot found by this id");
            }
        }

        public IEnumerable<Knot> GetAllKnots()
        {
            var knots = this.knotRepository.AllAsNoTracking().Select(x => new Knot
            {
                Name = x.Name,
                Type = x.Type,
                Description = x.Description,
                ImageUrls = x.ImageUrls,
            }).ToList();

            if (knots.Count > 0)
            {
                return knots;
            }
            else
            {
                throw new Exception("No knots found");
            }
        }

        public Knot GetById(string knotId)
        {
            var knot = this.knotRepository.All().Where(x => x.Id == knotId).FirstOrDefault();
            if (knot is not null)
            {
                return knot;
            }
            else
            {
                throw new Exception("No knot found by this id");
            }
        }

        public Knot GetByName(string knotName)
        {
            var knot = this.knotRepository.All().Where(x => x.Name == knotName).FirstOrDefault();
            if (knot is not null)
            {
                return knot;
            }
            else
            {
                throw new Exception("No knot found by this name!");
            }
        }

        public async Task UpdateKnotAsync(UpdateKnotInputModel updateKnotInputModel)
        {
            // Update videos
            var knot = this.GetById(updateKnotInputModel.KnotId);

            if (updateKnotInputModel.VideoUrl is not null)
            {
                knot.VideoUrl = updateKnotInputModel.VideoUrl;
            }

            if (knot is not null)
            {
                knot.Name = updateKnotInputModel.Name;
                knot.Type = updateKnotInputModel.Type;
                knot.Description = updateKnotInputModel.Description;

                if (updateKnotInputModel.Images != null)
                {
                    var cloudinary = Cloudinary();

                    foreach (var image in updateKnotInputModel.Images)
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
                            Folder = "FishApp/KnotImages/",
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

                        knot.ImageUrls.Add(imageUrl);
                    }

                    this.knotRepository.Update(knot);
                    await this.knotRepository.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("No knot found by this id");
                }
            }
        }
    }
}
