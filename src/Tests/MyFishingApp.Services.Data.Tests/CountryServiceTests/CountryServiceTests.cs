namespace MyFishingApp.Services.Data.Tests.CountryServiceTests
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
    using MyFishingApp.Services.Data.Countries;
    using MyFishingApp.Services.Data.InputModels;
    using Xunit;

    public class CountryServiceTests
    {
        [Fact]
        public async Task TestCreateCountry()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var countryService = new CountryService(repository);

            var model = new CountryInputModel
            {
                Name = "Bulgaria",
            };

            await countryService.CreateAsync(model);

            var result = repository.All().FirstOrDefaultAsync();

            Assert.Equal("Bulgaria", result.Result.Name);
        }

        [Fact]
        public async Task TestCreateCountryWithSameShouldThrowsException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var countryService = new CountryService(repository);

            var model = new CountryInputModel
            {
                Name = "Bulgaria",
            };

            await countryService.CreateAsync(model);

            await Assert.ThrowsAsync<Exception>(() => countryService.CreateAsync(model));
        }

        [Fact]
        public async Task TestDeleteCountryShouldThrowsExceptionWhenCountryIsNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var countryService = new CountryService(repository);

            await Assert.ThrowsAsync<Exception>(() => countryService.DeleteCountryAsync("2"));
        }

        [Fact]
        public async Task TestDeleteCountryShouldWorkProperly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            // await repository.AddAsync(new Country()
            // {
            //     Id = "1",
            //     Name = "Bulgaria",
            // });
            // await repository.AddAsync(new Country()
            // {
            //     Id = "2",
            //     Name = "Germany",
            // });
            // await repository.SaveChangesAsync();
            var countryService = new CountryService(repository);
            var model = new CountryInputModel
            {
                Name = "Bulgaria",
            };
            var model2 = new CountryInputModel
            {
                Name = "Germany",
            };
            await countryService.CreateAsync(model);
            await countryService.CreateAsync(model2);

            var country = repository.All().FirstOrDefault();
            await countryService.DeleteCountryAsync(country.Id);

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task TestGetByIdShouldWorkProperly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));
            await repository.AddAsync(new Country()
            {
                Id = "1",
                Name = "Bulgaria",
            });
            await repository.AddAsync(new Country()
            {
                Id = "2",
                Name = "Germany",
            });
            await repository.SaveChangesAsync();
            var countryService = new CountryService(repository);

            var country1 = countryService.FindCountryById("1");
            var country2 = countryService.FindCountryById("2");

            Assert.Equal("Bulgaria", country1.Name);
            Assert.Equal("Germany", country2.Name);
        }

        [Fact]
        public async Task TestGetByIdShouldThrowExceptionWhenNoCountryFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));
            await repository.AddAsync(new Country()
            {
                Id = "2",
                Name = "Germany",
            });
            await repository.SaveChangesAsync();
            var countryService = new CountryService(repository);

            Assert.Throws<Exception>(() => countryService.FindCountryById("3"));
        }

        [Fact]
        public async Task TestGetAllCountriesShouldWorkProperly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));
            await repository.AddAsync(new Country()
            {
                Id = "1",
                Name = "Bulgaria",
            });
            await repository.AddAsync(new Country()
            {
                Id = "2",
                Name = "Germany",
            });
            await repository.SaveChangesAsync();
            var countryService = new CountryService(repository);

            var countries = countryService.GetAll();

            Assert.Equal(2, countries.Count());
        }

        [Fact]
        public void TestGetAllCountriesShouldThrowExceptionWhenNoCountriesFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var countryService = new CountryService(repository);

            Assert.Throws<Exception>(() => countryService.GetAll());
        }

        [Fact]
        public async Task TestUpdateCountryShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var countryService = new CountryService(repository);

            var model = new CountryInputModel
            {
                Name = "Bulgaria",
            };

            await countryService.CreateAsync(model);
            var country = repository.All().FirstOrDefault();

            await countryService.UpdateAsync(country.Id, "India");

            Assert.NotNull(country);
            Assert.Equal("India", country.Name);
        }

        [Fact]
        public async Task TestUpdateCityShouldThrowsExceptionWhenCityIsNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var countryService = new CountryService(repository);

            var model = new CountryInputModel
            {
                Name = "Sofia",
            };

            await countryService.CreateAsync(model);
            await Assert.ThrowsAsync<Exception>(() => countryService.UpdateAsync("myCity", "myCountry"));
        }
    }
}
