﻿namespace MyFishingApp.Services.Data.Knots
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

        public KnotService(
            IDeletableEntityRepository<Knot> knotRepository)
        {
            this.knotRepository = knotRepository;
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
                ImageUrls = knotInputModel.ImageUrls,
            };

            if (knotInputModel.VideoUrl is not null)
            {
                knot.VideoUrl = knotInputModel.VideoUrl;
            }

            await this.knotRepository.AddAsync(knot);
            await this.knotRepository.SaveChangesAsync();

            Account account = new Account();
            account.ApiKey = "342347788652393";
            account.ApiSecret = "vekpkVY3cf729mldgq5aBrJmdbY";
            account.Cloud = "kocewwcloud";
            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription($"{knotInputModel.ImageUrl}"),
                PublicId = knot.Id,
                Folder = "FishApp/KnotImages/",
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            var url = uploadResult.Url.ToString();

            var imageUrl = new ImageUrls()
            {
                ImageUrl = url,
            };

            knot.ImageUrls.Add(imageUrl);
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

        public async Task UpdateKnotAsync(KnotInputModel knotInputModel, string knotId)
        {
            // Update images and Images URL
            var knot = this.GetById(knotId);
            if (knot is not null)
            {
                knot.Name = knotInputModel.Name;
                knot.Type = knotInputModel.Type;
                knot.Description = knotInputModel.Description;
                knot.ImageUrls = knotInputModel.ImageUrls;

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
