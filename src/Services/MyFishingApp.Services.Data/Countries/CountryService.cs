namespace MyFishingApp.Services.Data.Countries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;

    public class CountryService : ICountryService
    {
        private readonly IDeletableEntityRepository<Country> countryRepository;

        public CountryService(IDeletableEntityRepository<Country> countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public async Task CreateAsync(CountryInputModel countryInputModel)
        {
            var countryExists = this.countryRepository.All().Where(x => x.Name == countryInputModel.Name).FirstOrDefault();
            if (countryExists is not null)
            {
                throw new Exception("This country already exists");
            }

            var country = new Country()
            {
                Name = countryInputModel.Name,
            };

            await this.countryRepository.AddAsync(country);
            await this.countryRepository.SaveChangesAsync();
        }

        public async Task DeleteCountryAsync(string countryId)
        {
            var country = this.FindCountryById(countryId);
            if (country is not null)
            {
                this.countryRepository.Delete(country);
                await this.countryRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No country found  by this id");
            }
        }

        public Country FindCountryById(string countryId)
        {
            var country = this.countryRepository.All().Where(x => x.Id == countryId).FirstOrDefault();
            if (country is not null)
            {
                return country;
            }
            else
            {
                throw new Exception("No country found  by this id");
            }
        }

        public IEnumerable<Country> GetAll()
        {
            var countries = this.countryRepository.AllAsNoTracking().Select(x => new Country
            {
                Id = x.Id,
                Name = x.Name,
                CreatedOn = x.CreatedOn,
            }).ToList();

            if (countries.Count > 0)
            {
                return countries;
            }
            else
            {
                throw new Exception("No countries found");
            }
        }

        public async Task UpdateAsync(string countryId, string countryName)
        {
            var country = this.countryRepository.All().Where(x => x.Id == countryId).FirstOrDefault();
            if (country is not null)
            {
                country.Name = countryName;
                this.countryRepository.Update(country);
                await this.countryRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No country found  by this id");
            }
        }
    }
}
