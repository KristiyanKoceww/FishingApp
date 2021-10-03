﻿namespace MyFishingApp.Services.Data.Tests.CitiesServiceTests
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

            var repository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var cityService = new CityService(repository);

            var country = new Country()
            {
                Id = "1",
                Name = "Bulgaria",
            };

            var model = new CitiesInputModel
            {
                Name = "Sofia",
                Description = "Sofia is a city in Bulgarian",
                Country = country,
            };

            await cityService.CreateAsync(model);

            var result = repository.All().FirstOrDefault();

            Assert.Equal("Sofia", result.Name);
        }

        [Fact]
        public async Task TestCreateCountryWithSameShouldThrowsException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var cityService = new CityService(repository);

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

            await cityService.CreateAsync(model);

            await Assert.ThrowsAsync<Exception>(() => cityService.CreateAsync(model));
        }

        [Fact]
        public async Task TestGetByIdShouldWorkProperly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            await repository.AddAsync(new City()
            {
                Id = "1",
                Name = "Sofia",
            });
            await repository.AddAsync(new City()
            {
                Id = "2",
                Name = "Burgas",
            });
            await repository.SaveChangesAsync();
            var cityService = new CityService(repository);

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

            var repository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            await repository.AddAsync(new City()
            {
                Id = "2",
                Name = "Sofia",
            });
            await repository.SaveChangesAsync();
            var cityService = new CityService(repository);

            Assert.Throws<Exception>(() => cityService.FindCityById("3"));
        }

        [Fact]
        public async Task TestGetByNameShouldWorkProperly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            await repository.AddAsync(new City()
            {
                Id = "1",
                Name = "Sofia",
            });
            await repository.AddAsync(new City()
            {
                Id = "2",
                Name = "Burgas",
            });
            await repository.SaveChangesAsync();
            var cityService = new CityService(repository);

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

            var repository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            await repository.AddAsync(new City()
            {
                Id = "2",
                Name = "Sofia",
            });
            await repository.SaveChangesAsync();
            var cityService = new CityService(repository);

            Assert.Throws<Exception>(() => cityService.FindCityByName("Burgas"));
        }

        [Fact]
        public async Task TestGetAllCitiesShouldWorkProperly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));
            await repository.AddAsync(new City()
            {
                Id = "1",
                Name = "Sofia",
            });
            await repository.AddAsync(new City()
            {
                Id = "2",
                Name = "Burgas",
            });
            await repository.SaveChangesAsync();
            var cityService = new CityService(repository);

            var cities = cityService.GetAllCities();

            Assert.Equal(2, cities.Count());
        }

        [Fact]
        public void TestGetAllCitiesShouldThrowExceptionWhenNoCitiesAreFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<City>(new ApplicationDbContext(options.Options));

            var cityService = new CityService(repository);

            Assert.Throws<Exception>(() => cityService.GetAllCities());
        }
    }
}
