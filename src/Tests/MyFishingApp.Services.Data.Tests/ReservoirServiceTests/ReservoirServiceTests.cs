namespace MyFishingApp.Services.Data.Tests.ReservoirServiceTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyFishingApp.Data;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Data.Repositories;
    using MyFishingApp.Services.Data.Dam;
    using MyFishingApp.Services.Data.InputModels;
    using Xunit;

    public class ReservoirServiceTests
    {
        [Fact]
        public async Task TestCreateReservoir()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservoirRepository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var reservoirService = new ReservoirService(reservoirRepository, cityRepository);

            var country = new Country()
            {
                Id = "1",
                Name = "BG",
            };

            var city = new City()
            {
                CountryId = "1",
                Country = country,
                CountryName = "Name",
                Name = "Sofia",
            };

            var model = new CreateReservoirInputModel
            {
                Name = "Iskar",
                City = city,
                Description = "Very big",
                Latitude = "222",
                Longitude = "222",
                Type = "Large",
            };

            await reservoirService.CreateReservoir(model);

            var result = reservoirRepository.All().FirstOrDefaultAsync();

            Assert.Equal("Iskar", result.Result.Name);
        }

        [Fact]
        public async Task TestCreate2ReservoirWithSameNamesShouldThrowsException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservoirRepository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var reservoirService = new ReservoirService(reservoirRepository, cityRepository);

            var model = new CreateReservoirInputModel
            {
                Name = "Iskar",
            };

            var model2 = new CreateReservoirInputModel
            {
                Name = "Iskar",
            };

            await reservoirService.CreateReservoir(model);
            await Assert.ThrowsAsync<Exception>(() => reservoirService.CreateReservoir(model2));
        }

        [Fact]
        public async Task GetReservoirById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservoirRepository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var reservoirService = new ReservoirService(reservoirRepository, cityRepository);

            reservoirRepository.AddAsync(new Reservoir { Id = "1", Name = "Iskar" }).GetAwaiter().GetResult();
            reservoirRepository.AddAsync(new Reservoir { Id = "2", Name = "Dunav" }).GetAwaiter().GetResult();
            await reservoirRepository.SaveChangesAsync();

            var res = reservoirService.GetById("1");
            var res2 = reservoirService.GetById("2");

            Assert.Equal("Iskar", res.Name);
            Assert.Equal("Dunav", res2.Name);
        }

        [Fact]
        public void GetReservoirByIdShouldThrowsExceptionIfDoesntExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var reservoirRepository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var reservoirService = new ReservoirService(reservoirRepository, cityRepository);

            Assert.Throws<Exception>(() => reservoirService.GetById("1"));
        }

        [Fact]
        public async Task TestGetAllReservoirs()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Reservoir { Id = "1", Name = "Iskar" }).GetAwaiter().GetResult();
            repository.AddAsync(new Reservoir { Id = "2", Name = "Dunav" }).GetAwaiter().GetResult();
            await repository.SaveChangesAsync();

            var reservoirRepository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var reservoirService = new ReservoirService(reservoirRepository, cityRepository);

            var res = reservoirService.GetAllReservoirs();

            Assert.Equal(2, res.Count());
        }

        [Fact]
        public void GetAllReservoirsShouldThrowsExceptionWhenThereAreNoReservoirsFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var reservoirRepository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var reservoirService = new ReservoirService(reservoirRepository, cityRepository);

            Assert.Throws<Exception>(() => reservoirService.GetAllReservoirs());
        }

        [Fact]
        public async Task TestDeleteReservoir()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Reservoir { Id = "1", Name = "Iskar" }).GetAwaiter().GetResult();
            repository.AddAsync(new Reservoir { Id = "2", Name = "Dunav" }).GetAwaiter().GetResult();
            await repository.SaveChangesAsync();

            var reservoirRepository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var reservoirService = new ReservoirService(reservoirRepository, cityRepository);

            await reservoirService.DeleteReservoir("1");

            Assert.Equal(1, reservoirRepository.All().Count());
        }

        [Fact]
        public async Task TestDeleteReservoirThrowsExceptionWhenIsNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Reservoir { Id = "1", Name = "Iskar" }).GetAwaiter().GetResult();
            repository.AddAsync(new Reservoir { Id = "2", Name = "Dunav" }).GetAwaiter().GetResult();
            await repository.SaveChangesAsync();

            var reservoirRepository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var reservoirService = new ReservoirService(reservoirRepository, cityRepository);

            await Assert.ThrowsAsync<Exception>(() => reservoirService.DeleteReservoir("3"));
        }

        [Fact]
        public async Task TestUpdateReservoir()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Reservoir { Id = "1", Name = "Iskar" }).GetAwaiter().GetResult();
            await repository.SaveChangesAsync();

            var reservoirRepository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var reservoirService = new ReservoirService(reservoirRepository, cityRepository);

            var model = new UpdateReservoirInputModel
            {
                Name = "Dunav",
            };

            await reservoirService.UpdateReservoir(model);
            var res = repository.All().FirstOrDefault();

            Assert.Equal("Dunav", res.Name);
        }

        [Fact]
        public async Task TestUpdateReservoirShouldDoNothingWhenItsNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Reservoir { Id = "1", Name = "Iskar" }).GetAwaiter().GetResult();
            await repository.SaveChangesAsync();

            var reservoirRepository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var reservoirService = new ReservoirService(reservoirRepository, cityRepository);

            var model = new UpdateReservoirInputModel
            {
                Name = "Dunav",
            };

            await Assert.ThrowsAsync<Exception>(() => reservoirService.UpdateReservoir(model));
        }

        [Fact]
        public async Task GetReservoirByNameShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Reservoir { Id = "1", Name = "Iskar" }).GetAwaiter().GetResult();
            repository.AddAsync(new Reservoir { Id = "2", Name = "Dunav" }).GetAwaiter().GetResult();
            await repository.SaveChangesAsync();
            var reservoirRepository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var reservoirService = new ReservoirService(reservoirRepository, cityRepository);

            var res = reservoirService.GetByName("Iskar");
            var res2 = reservoirService.GetByName("Dunav");

            Assert.Equal("Iskar", res.Name);
            Assert.Equal("Dunav", res2.Name);
        }

        [Fact]
        public void GetReservoirByNameShouldThrowsExceptionIfDoesntExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var reservoirRepository = new EfDeletableEntityRepository<Reservoir>(new ApplicationDbContext(options.Options));
            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var reservoirService = new ReservoirService(reservoirRepository, cityRepository);

            Assert.Throws<Exception>(() => reservoirService.GetByName("myReservoir"));
        }
    }
}
