namespace MyFishingApp.Services.Data.FishServ
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.FishInputModels;

    public interface IFishService
    {
        Task CreateAsync(FishInputModel createFishInputModel);

        Task DeleteFish(string fishId);

        Task UpdateFish(UpdateFishInputModel updateFishInputModel);

        Fish GetById(string fishId);

        Fish GetByName(string fishName);

        IEnumerable<Fish> GetAllFish();
    }
}
