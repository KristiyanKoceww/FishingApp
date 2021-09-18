namespace MyFishingApp.Services.Data.Countries
{
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

        public async Task Create(CountryInputModel countryInputModel)
        {
            var country = new Country()
            {
                Name = countryInputModel.Name,
            };

            await this.countryRepository.AddAsync(country);
            await this.countryRepository.SaveChangesAsync();
        }

        public Country FindCountryById(string countryId)
        {
            var country = this.countryRepository.All().Where(x => x.Id == countryId).FirstOrDefault();
            return country;
        }

        public IEnumerable<Country> GetAll()
        {
            var countries = this.countryRepository.AllAsNoTracking().Select(x => new Country
            {
                Id = x.Id,
                Name = x.Name,
                CreatedOn = x.CreatedOn,
            }).ToList();

            return countries;
        }
    }
}
