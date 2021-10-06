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
            var knotService = new KnotService(repository);

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
        public async Task TestCreate2KnotsWithSameNamesShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var knotService = new KnotService(repository);

            var model = new KnotInputModel
            {
                Name = "Knot",
                Type = "Simple",
                Description = "Simple knot",
            };

            await knotService.CreateKnotAsync(model);

            await Assert.ThrowsAsync<Exception>(() => knotService.CreateKnotAsync(model));
        }

        [Fact]
        public async Task GetKnotById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));

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

            var knotService = new KnotService(repository);

            var res = knotService.GetById("1");
            var res2 = knotService.GetById("2");

            Assert.Equal("8", res.Name);
            Assert.Equal("So Simple", res2.Name);
        }

        [Fact]
        public void GetKnotByIdShouldThrowExceptionIfDoesntExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var knotService = new KnotService(repository);

            Assert.Throws<Exception>(() => knotService.GetById("1"));
        }

        [Fact]
        public async Task TestGetAllKnots()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));

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

            var knotService = new KnotService(repository);
            var res = knotService.GetAllKnots();

            Assert.Equal(2, res.Count());
        }

        [Fact]
        public void GetAllKnotsShouldThrowExceptionIfNoKnotsFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var knotService = new KnotService(repository);

            Assert.Throws<Exception>(() => knotService.GetAllKnots());
        }

        [Fact]
        public async Task TestDeleteKnot()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));

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

            var knotService = new KnotService(repository);

            await knotService.DeleteKnotAsync("1");

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task TestDeleteKnotShouldThrowExceptionWhenIsNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Knot
            {
                Id = "1",
                Name = "8",
                Type = "Simple",
                Description = "Simple knot",
            }).GetAwaiter().GetResult();

            await repository.SaveChangesAsync();

            var knotService = new KnotService(repository);

            await Assert.ThrowsAsync<Exception>(() => knotService.DeleteKnotAsync("2"));
        }

        [Fact]
        public async Task TestUpdateReservoir()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Knot
            {
                Id = "1",
                Name = "8",
                Type = "Simple",
                Description = "Simple knot",
            }).GetAwaiter().GetResult();

            await repository.SaveChangesAsync();

            var knotService = new KnotService(repository);

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

            repository.AddAsync(new Knot
            {
                Id = "1",
                Name = "8",
                Type = "Simple",
                Description = "Simple knot",
            }).GetAwaiter().GetResult();

            await repository.SaveChangesAsync();

            var knotService = new KnotService(repository);

            var model = new KnotInputModel
            {
                Name = "Simple Knot",
            };

            await Assert.ThrowsAsync<Exception>(() => knotService.UpdateKnotAsync(model, "2"));
        }

        [Fact]
        public async Task GetKnotByNameShouldWorkCorectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));

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

            var knotService = new KnotService(repository);

            var res = knotService.GetByName("8");
            var res2 = knotService.GetByName("So Simple");

            Assert.Equal("8", res.Name);
            Assert.Equal("So Simple", res2.Name);
        }

        [Fact]
        public void GetKnotByNameShouldThrowExceptionIfDoesntExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Knot>(new ApplicationDbContext(options.Options));
            var knotService = new KnotService(repository);

            Assert.Throws<Exception>(() => knotService.GetByName("myKnot"));
        }

    }
}
