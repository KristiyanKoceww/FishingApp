namespace MyFishingApp.Services.Data.Tests.KnotServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyFishingApp.Data;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Data.Repositories;
    using MyFishingApp.Services.Data.InputModels;
    using MyFishingApp.Services.Data.Knots;
    using Xunit;

    public class KnotServiceTests
    {
        [Fact]
        public async Task TestCreateKnot()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var knotService = new KnotService(repository, imageRepository);


            var model = new KnotInputModel
            {
                Name = "8",
                Type = "Simple",
                Description = "Simple knot",
            };

            await knotService.CreateKnotAsync(model);

            var result = repository.All().FirstOrDefaultAsync();

            Assert.Equal("8", result.Result.Name);
        }

        [Fact]
        public async Task TestCreate2KnotsWithSameNames()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var knotService = new KnotService(repository, imageRepository);

            var model = new KnotInputModel
            {
                Name = "8",
                Type = "Simple",
                Description = "Simple knot",
            };

            var model2 = new KnotInputModel
            {
                Name = "8",
                Type = "Simple",
                Description = "Simple knot",
            };

            await knotService.CreateKnotAsync(model);
            await knotService.CreateKnotAsync(model2);

            var result = repository.All().Count();

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task GetKnotById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Knot
            {
                Id = "1",
                Name = "8",
                Type = "Simple",
                Description = "Simple knot",
            }).GetAwaiter().GetResult();
            repository.AddAsync(new Knot
            {
                Id = "2",
                Name = "So Simple",
                Type = "Simple",
                Description = "Simple knot",
            }).GetAwaiter().GetResult();
            await repository.SaveChangesAsync();

            var knotService = new KnotService(repository, imageRepository);

            var res = knotService.GetById("1");
            var res2 = knotService.GetById("2");

            Assert.Equal("8", res.Name);
            Assert.Equal("So Simple", res2.Name);
        }

        [Fact]
        public void GetKnotByIdShouldReturnNullIfDoesntExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var knotService = new KnotService(repository, imageRepository);

            var res = knotService.GetById("1");

            Assert.Null(res);
        }

        [Fact]
        public async Task TestGetAllKnots()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Knot
            {
                Name = "8",
                Type = "Simple",
                Description = "Simple knot",
            }).GetAwaiter().GetResult();
            repository.AddAsync(new Knot
            {
                Name = "So Simple",
                Type = "Simple",
                Description = "Simple knot",
            }).GetAwaiter().GetResult();
            await repository.SaveChangesAsync();

            var knotService = new KnotService(repository, imageRepository);
            var res = knotService.GetAllKnots();

            Assert.Equal(2, res.Count());
        }

        [Fact]
        public void GetAllKnotsShouldReturnZero()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var knotService = new KnotService(repository, imageRepository);

            var res = knotService.GetAllKnots();

            Assert.Empty(res);
        }

        [Fact]
        public async Task TestDeleteKnot()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Knot
            {
                Id = "1",
                Name = "8",
                Type = "Simple",
                Description = "Simple knot",
            }).GetAwaiter().GetResult();
            repository.AddAsync(new Knot
            {
                Id = "2",
                Name = "So Simple",
                Type = "Simple",
                Description = "Simple knot",
            }).GetAwaiter().GetResult();
            await repository.SaveChangesAsync();

            var knotService = new KnotService(repository, imageRepository);

            var res = knotService.DeleteKnotAsync("1");

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task TestDeleteKnotShouldDoNothingWhenIsNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Knot
            {
                Id = "1",
                Name = "8",
                Type = "Simple",
                Description = "Simple knot",
            }).GetAwaiter().GetResult();

            await repository.SaveChangesAsync();

            var knotService = new KnotService(repository, imageRepository);

            var res = knotService.DeleteKnotAsync("2");

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task TestUpdateReservoir()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Knot
            {
                Id = "1",
                Name = "8",
                Type = "Simple",
                Description = "Simple knot",
            }).GetAwaiter().GetResult();

            await repository.SaveChangesAsync();

            var knotService = new KnotService(repository, imageRepository);

            var model = new KnotInputModel
            {
                Name = "Simple Knot",
            };

            await knotService.UpdateKnotAsync(model, "1");

            var res = repository.All().FirstOrDefault();

            Assert.Equal("Simple Knot", res.Name);
        }

        [Fact]
        public async Task TestUpdateReservoirShouldDoNothingWhenItsNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Knot
            {
                Id = "1",
                Name = "8",
                Type = "Simple",
                Description = "Simple knot",
            }).GetAwaiter().GetResult();

            await repository.SaveChangesAsync();

            var knotService = new KnotService(repository, imageRepository);

            var model = new KnotInputModel
            {
                Name = "Simple Knot",
            };

            await knotService.UpdateKnotAsync(model, "2");

            var res = repository.All().FirstOrDefault();

            Assert.Equal("8", res.Name);
        }
    }
}
