namespace MyFishingApp.Services.Data.Tests.CitiesServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AirportSystem.Services.Data.CitiesAndCountries;
    using Microsoft.EntityFrameworkCore;
    using MyFishingApp.Data;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Data.Repositories;
    using MyFishingApp.Services.Data.InputModels;
    using Xunit;

    public class CitiesServiceTests
    {
        [Fact]
        public async Task TestCreateCity()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var options2 = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            var countryRepository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options2.Options));

            var cityService = new CityService(cityRepository, countryRepository);

            var country = new Country()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Bulgaria",
            };

            await countryRepository.AddAsync(country);
            await countryRepository.SaveChangesAsync();

            var model = new CitiesInputModel
            {
                Name = "Sofia",
                Description = "Sofia is a city in Bulgaria",
                Country = country,
                CountryId = country.Id,
            };

            await cityService.CreateAsync(model);

            var result = cityRepository.All().FirstOrDefault();

            Assert.Equal("Sofia", result.Name);
        }

        [Fact]
        public async Task TestCreateCityWithSameNameShouldThrowsException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            var countryRepository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var cityService = new CityService(cityRepository, countryRepository);

            var country = new Country()
            {
                Id = "1",
                Name = "Bulgaria",
            };
            var model = new CitiesInputModel
            {
                Name = "Sofia",
                Description = "Sofia is a city in Bulgaria",
                Country = country,
            };

            await cityRepository.AddAsync(new City() { Name = "Sofia", Country = country, Description = " " });
            await cityRepository.SaveChangesAsync();
            await Assert.ThrowsAsync<Exception>(() => cityService.CreateAsync(model));
        }

        [Fact]
        public async Task TestDeleteCityShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            var countryRepository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var cityService = new CityService(cityRepository, countryRepository);

            await cityRepository.AddAsync(new City()
            {
                Id = "1",
                Name = "Sofia",
            });
            await cityRepository.AddAsync(new City()
            {
                Id = "2",
                Name = "Burgas",
            });

            await cityRepository.SaveChangesAsync();

            var city = cityRepository.All().Where(x => x.Name == "Sofia").FirstOrDefault();
            await cityService.DeleteAsync(city.Id);
            var result = cityRepository.All().Count();

            Assert.Equal("Sofia", city.Name);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task TestDeleteCityShouldThrowsExceptionWhenCityIsNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            var countryRepository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var cityService = new CityService(cityRepository, countryRepository);

            await cityRepository.AddAsync(new City()
            {
                Id = "1",
                Name = "Sofia",
            });
            await cityRepository.AddAsync(new City()
            {
                Id = "2",
                Name = "Burgas",
            });

            await cityRepository.SaveChangesAsync();
            var result = cityRepository.All().Count();

            await Assert.ThrowsAsync<Exception>(() => cityService.DeleteAsync(Guid.NewGuid().ToString()));
            Assert.Equal(2, result);
        }

        [Fact]
        public async Task TestGetByIdShouldWorkProperly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            var countryRepository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var cityService = new CityService(cityRepository, countryRepository);
            await cityRepository.AddAsync(new City()
            {
                Id = "1",
                Name = "Sofia",
            });
            await cityRepository.AddAsync(new City()
            {
                Id = "2",
                Name = "Burgas",
            });
            await cityRepository.SaveChangesAsync();

            var city1 = cityService.FindCityById("1");
            var city2 = cityService.FindCityById("2");

            Assert.Equal("Sofia", city1.Name);
            Assert.Equal("Burgas", city2.Name);
        }

        [Fact]
        public async Task TestGetByIdShouldThrowExceptionWhenNoCityIsFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            var countryRepository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var cityService = new CityService(cityRepository, countryRepository);

            await cityRepository.AddAsync(new City()
            {
                Id = "2",
                Name = "Sofia",
            });
            await cityRepository.SaveChangesAsync();

            Assert.Throws<Exception>(() => cityService.FindCityById("3"));
        }

        [Fact]
        public async Task TestGetByNameShouldWorkProperly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            var countryRepository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var cityService = new CityService(cityRepository, countryRepository);
            await cityRepository.AddAsync(new City()
            {
                Id = "1",
                Name = "Sofia",
            });
            await cityRepository.AddAsync(new City()
            {
                Id = "2",
                Name = "Burgas",
            });
            await cityRepository.SaveChangesAsync();

            var city1 = cityService.FindCityByName("Sofia");
            var city2 = cityService.FindCityByName("Burgas");

            Assert.Equal("1", city1.Id);
            Assert.Equal("2", city2.Id);
        }

        [Fact]
        public async Task TestGetByNameShouldThrowExceptionWhenNoCityIsFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            var countryRepository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var cityService = new CityService(cityRepository, countryRepository);
            await cityRepository.AddAsync(new City()
            {
                Id = "2",
                Name = "Sofia",
            });
            await cityRepository.SaveChangesAsync();

            Assert.Throws<Exception>(() => cityService.FindCityByName("Burgas"));
        }

        [Fact]
        public async Task TestGetAllCitiesShouldWorkProperly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            var countryRepository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var cityService = new CityService(cityRepository, countryRepository);

            await cityRepository.AddAsync(new City()
            {
                Id = "1",
                Name = "Sofia",
            });
            await cityRepository.AddAsync(new City()
            {
                Id = "2",
                Name = "Burgas",
            });
            await cityRepository.SaveChangesAsync();

            var cities = cityService.GetAllCities();

            Assert.Equal(2, cities.Count());
        }

        [Fact]
        public void TestGetAllCitiesShouldThrowExceptionWhenNoCitiesAreFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            var countryRepository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var cityService = new CityService(cityRepository, countryRepository);

            Assert.Throws<Exception>(() => cityService.GetAllCities());
        }

        [Fact]
        public async Task TestUpdateCityShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var options2 = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            var countryRepository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options2.Options));

            var cityService = new CityService(cityRepository, countryRepository);

            var country = new Country()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Bulgaria",
            };

            await countryRepository.AddAsync(country);

            await cityRepository.AddAsync(new City()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Sofia",
                Description = "null",
            });

            await cityRepository.SaveChangesAsync();
            await countryRepository.SaveChangesAsync();

            var city = cityRepository.All().FirstOrDefault();

            var model = new CitiesInputModel
            {
                Name = "Sofia2",
                Description = "city",
                Country = country,
            };

            await cityService.UpdateAsync(city.Id, model);

            Assert.NotNull(city);
            Assert.Equal("city", city.Description);
            Assert.Equal("Sofia2", city.Name);
        }

        [Fact]
        public async Task TestUpdateCityShouldThrowsExceptionWhenCityIsNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var cityRepository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            var countryRepository = new EfDeletableEntityRepository<Country>(new ApplicationDbContext(options.Options));

            var cityService = new CityService(cityRepository, countryRepository);

            var country = new Country()
            {
                Id = "1",
                Name = "Bulgaria",
            };

            await countryRepository.AddAsync(country);
            await cityRepository.AddAsync(new City()
            {
                Id = "1",
                Name = "Sofia",
            });

            await cityRepository.SaveChangesAsync();
            await countryRepository.SaveChangesAsync();
            var city = cityRepository.All().FirstOrDefault();

            var model = new CitiesInputModel
            {
                Name = "Sofia2",
                Description = "city",
                Country = country,
            };

            await Assert.ThrowsAsync<Exception>(() => cityService.UpdateAsync("myCity", model));
        }
    }
}
