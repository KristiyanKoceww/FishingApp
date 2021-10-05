namespace MyFishingApp.Services.Data.Cities
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;

    public interface ICityService
    {
        Task CreateAsync(CitiesInputModel citiesInputModel);

        Task UpdateAsync(string cityId, CitiesInputModel citiesInputModel);

        Task DeleteAsync(string cityId);

        IEnumerable<City> GetAllCities();

        City FindCityById(string cityId);

        City FindCityByName(string cityName);
    }
}
