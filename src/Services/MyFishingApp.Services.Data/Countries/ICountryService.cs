namespace MyFishingApp.Services.Data.Countries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;

    public interface ICountryService
    {
        Task CreateAsync(CountryInputModel countryInputModel);

        Task UpdateAsync(string countryId, string countryName);

        IEnumerable<Country> GetAll();

        Country FindCountryById(string countryId);

        Task DeleteCountryAsync(string countryId);
    }
}
