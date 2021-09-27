namespace MyFishingApp.Services.Data.Tests.FishServiceTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyFishingApp.Data;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Data.Repositories;
    using MyFishingApp.Services.Data.FishServ;
    using MyFishingApp.Services.Data.InputModels.FishInputModels;
    using Xunit;

    public class FishServiceTests
    {
        [Fact]
        public async Task TestCreateFish()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Fish>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var fishService = new FishService(repository, imageRepository);


            var model = new FishInputModel
            {
                Name = "Carp",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 100,
                Weight = 10,
                Nutrition = "null",
                Tips = "Very hard to catch",
            };

            await fishService.CreateAsync(model);

            var result = repository.All().FirstOrDefaultAsync();

            Assert.Equal("Carp", result.Result.Name);
        }

        [Fact]
        public async Task TestCreateFishWithSameNameShouldThrowsException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Fish>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var fishService = new FishService(repository, imageRepository);

            var model = new FishInputModel
            {
                Name = "Carp",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 100,
                Weight = 10,
                Nutrition = "null",
                Tips = "Very hard to catch",
            };

            var model2 = new FishInputModel
            {
                Name = "Carp",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 100,
                Weight = 10,
                Nutrition = "null",
                Tips = "Very hard to catch",
            };

            await fishService.CreateAsync(model);
            await Assert.ThrowsAsync<System.Exception>(() => fishService.CreateAsync(model2));
            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task TestDeleteFish()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Fish>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var fishService = new FishService(repository, imageRepository);

            var model = new FishInputModel
            {
                Name = "Carp",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 100,
                Weight = 10,
                Nutrition = "null",
                Tips = "Very hard to catch",
            };

            await fishService.CreateAsync(model);

            var fish = repository.All().FirstOrDefault();

            await fishService.DeleteFish(fish.Id);

            Assert.Equal(0, repository.All().Count());
        }

        [Fact]
        public async Task TestDeleteFishShouldDoNothingIfNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Fish>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var fishService = new FishService(repository, imageRepository);

            var model = new FishInputModel
            {
                Name = "Carp",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 100,
                Weight = 10,
                Nutrition = "null",
                Tips = "Very hard to catch",
            };

            await fishService.CreateAsync(model);

            await fishService.DeleteFish("2");

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task TestGetAllFishShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Fish>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var fishService = new FishService(repository, imageRepository);

            await repository.AddAsync(new Fish()
            {
                Name = "Carp",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 100,
                Weight = 10,
                Nutrition = "null",
                Tips = "Very hard to catch",
            });

            await repository.AddAsync(new Fish()
            {
                Name = "Ton",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 100,
                Weight = 10,
                Nutrition = "null",
                Tips = "Very hard to catch",
            });

            await repository.SaveChangesAsync();

            var fish = fishService.GetAllFish();

            Assert.Equal(2, fish.Count());
        }

        [Fact]
        public void TestGetAllFishShouldReturnZeroWhenCollectionIsEmpty()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Fish>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var fishService = new FishService(repository, imageRepository);

            var fish = fishService.GetAllFish();

            Assert.Empty(fish);
        }

        [Fact]
        public async Task TestGetFishByIdShouldReturnCorrectFish()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Fish>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var fishService = new FishService(repository, imageRepository);

            await repository.AddAsync(new Fish()
            {
                Id = "1",
                Name = "Carp",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 100,
                Weight = 10,
                Nutrition = "null",
                Tips = "Very hard to catch",
            });

            await repository.AddAsync(new Fish()
            {
                Id = "2",
                Name = "Ton",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 100,
                Weight = 10,
                Nutrition = "null",
                Tips = "Very hard to catch",
            });

            await repository.SaveChangesAsync();

            var fish1 = fishService.GetById("1");
            var fish2 = fishService.GetById("2");

            Assert.Equal("Carp", fish1.Name);
            Assert.Equal("Ton", fish2.Name);
        }

        [Fact]
        public async Task TestGetFishByIdShouldReturnNullWhenNoMatch()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Fish>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var fishService = new FishService(repository, imageRepository);

            await repository.AddAsync(new Fish()
            {
                Id = "1",
                Name = "Carp",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 100,
                Weight = 10,
                Nutrition = "null",
                Tips = "Very hard to catch",
            });

            await repository.AddAsync(new Fish()
            {
                Id = "2",
                Name = "Ton",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 100,
                Weight = 10,
                Nutrition = "null",
                Tips = "Very hard to catch",
            });

            await repository.SaveChangesAsync();

            var fish1 = fishService.GetById("3");
            var fish2 = fishService.GetById("4");

            Assert.Null(fish2);
            Assert.Null(fish1);
        }

        [Fact]
        public async Task TestUpdateFish()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Fish>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var fishService = new FishService(repository, imageRepository);

            await repository.AddAsync(new Fish()
            {
                Id = "1",
                Name = "Carp",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 100,
                Weight = 10,
                Nutrition = "null",
                Tips = "Very hard to catch",
            });

            await repository.SaveChangesAsync();

            var model = new FishInputModel
            {
                Name = "Ton",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 200,
                Weight = 100,
                Nutrition = "null",
                Tips = "Very hard to catch",
            };

            await fishService.UpdateFish(model, "1");

            var fish = repository.All().FirstOrDefault();

            Assert.Equal("Ton", fish.Name);
            Assert.Equal(200, fish.Lenght);
        }

        [Fact]
        public async Task TestUpdateFishShouldDoNothingIfNotFishFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Fish>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<Image>(new ApplicationDbContext(options.Options));

            var fishService = new FishService(repository, imageRepository);

            await repository.AddAsync(new Fish()
            {
                Id = "1",
                Name = "Carp",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 100,
                Weight = 10,
                Nutrition = "null",
                Tips = "Very hard to catch",
            });

            await repository.SaveChangesAsync();

            var model = new FishInputModel
            {
                Name = "Ton",
                Habittat = "Natural",
                Description = "Simple knot",
                Lenght = 200,
                Weight = 100,
                Nutrition = "null",
                Tips = "Very hard to catch",
            };

            await fishService.UpdateFish(model, "2");

            var fish = repository.All().FirstOrDefault();

            Assert.Equal("Carp", fish.Name);
            Assert.Equal(100, fish.Lenght);
        }
    }
}
