namespace AirportSystem.Services.Data.CitiesAndCountries
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
        private readonly IDeletableEntityRepository<Country> countryRepository;

        public CityService(
            IDeletableEntityRepository<City> cityRepository,
            IDeletableEntityRepository<Country> countryRepository)
        {
            this.cityRepository = cityRepository;
            this.countryRepository = countryRepository;
        }

        public async Task CreateAsync(CitiesInputModel citiesInputModel)
        {
            var cityExists = this.cityRepository.All().Where(x => x.Name == citiesInputModel.Name).FirstOrDefault();
            if (cityExists is not null)
            {
                throw new Exception("This city already exists");
            }

            var country = this.countryRepository.All().Where(x => x.Id == citiesInputModel.CountryId).FirstOrDefault();
            if (country is null)
            {
                throw new Exception("No country found by this id");
            }

            var city = new City()
            {
                Name = citiesInputModel.Name,
                Description = citiesInputModel.Description,
                Country = country,
                CountryName = country.Name,
                CountryId = country.Id,
            };

            await this.cityRepository.AddAsync(city);
            await this.cityRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string cityId)
        {
            var city = this.cityRepository.All().Where(x => x.Id == cityId).FirstOrDefault();

            if (city is not null)
            {
                this.cityRepository.Delete(city);
                await this.cityRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No city found  by this id");
            }
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
                throw new Exception("No city found  by this id!");
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
                throw new Exception("No city found  by this name!");
            }
        }

        public IEnumerable<City> GetAllCities()
        {
            var cities = this.cityRepository.AllAsNoTracking().Select(x => new City()
            {
                Id = x.Id,
                Name = x.Name,
                CountryName = x.CountryName,
                CountryId = x.CountryId,
                CreatedOn = x.CreatedOn,
                Description = x.Description,
                IsDeleted = x.IsDeleted,
                DeletedOn = x.DeletedOn,
                ModifiedOn = x.ModifiedOn,
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

        public IEnumerable<City> GetCitiesByCount(int count)
        {
            var cities = this.cityRepository.AllAsNoTracking().Select(x => new City()
            {
                Id = x.Id,
                Name = x.Name,
                CountryName = x.CountryName,
                CountryId = x.CountryId,
                CreatedOn = x.CreatedOn,
                Description = x.Description,
                IsDeleted = x.IsDeleted,
                DeletedOn = x.DeletedOn,
                ModifiedOn = x.ModifiedOn,
            }).Take(count).ToList();

            return cities;
        }

        public async Task UpdateAsync(string cityId, CitiesInputModel citiesInputModel)
        {
            var city = this.cityRepository.All().Where(x => x.Id == cityId).FirstOrDefault();

            if (city is not null)
            {
                city.Name = citiesInputModel.Name;
                city.Description = citiesInputModel.Description;
                city.Country = citiesInputModel.Country;
                city.CountryName = citiesInputModel.Country.Name;
                this.cityRepository.Update(city);
                await this.cityRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No city found  by this id");
            }
        }
    }
}
