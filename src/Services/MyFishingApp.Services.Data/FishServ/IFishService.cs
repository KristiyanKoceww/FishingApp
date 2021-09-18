﻿namespace MyFishingApp.Services.Data.FishServ
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

        Task UpdateFish(FishInputModel updateFishInputModel, string fishId);

        Fish GetById(string reservoirId);

        IEnumerable<Fish> GetAllFish();
    }
}
