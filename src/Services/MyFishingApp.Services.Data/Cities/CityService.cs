﻿namespace AirportSystem.Services.Data.CitiesAndCountries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.Cities;
    using MyFishingApp.Services.Data.InputModels;

    public class CityService : ICityService
    {
        private readonly IDeletableEntityRepository<City> cityRepository;

        public CityService(IDeletableEntityRepository<City> cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public async Task CreateAsync(CitiesInputModel citiesInputModel)
        {
            var cityExists = this.cityRepository.All().Where(x => x.Name == citiesInputModel.Name).FirstOrDefault();
            if (cityExists is not null)
            {
                throw new Exception("This city already exists");
            }

            var city = new City()
            {
                Name = citiesInputModel.Name,
                Description = citiesInputModel.Description,
                Country = citiesInputModel.Country,
                CountryName = citiesInputModel.Country.Name,
                CountryId = citiesInputModel.Country.Id,
            };

            await this.cityRepository.AddAsync(city);
            await this.cityRepository.SaveChangesAsync();
        }

        public City FindCityById(string cityId)
        {
            var city = this.cityRepository.All().Where(x => x.Id == cityId).FirstOrDefault();

            if (city is not null)
            {
                return city;
            }
            else
            {
                throw new Exception("No city found  by this id");
            }
        }

        public City FindCityByName(string cityName)
        {
            var city = this.cityRepository.All().Where(x => x.Name == cityName).FirstOrDefault();
            if (city is not null)
            {
                return city;
            }
            else
            {
                throw new Exception("No city found  by this id");
            }
        }

        public IEnumerable<City> GetAllCities()
        {
            var cities = this.cityRepository.AllAsNoTracking().Select(x => new City()
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
            }).ToList();

            if (cities.Count > 0)
            {
                return cities;
            }
            else
            {
                throw new Exception("No cities found");
            }
        }
    }
}
