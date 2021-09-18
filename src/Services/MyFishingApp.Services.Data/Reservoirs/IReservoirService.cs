namespace MyFishingApp.Services.Data
{
    using System.Collections.Generic;

    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;

    public interface IReservoirService
    {
        void CreateReservoir(CreateReservoirInputModel createReservoirInputModel);

        void DeleteReservoir(string reservoirId);

        void UpdateReservoir(string reservoirId);

        IEnumerable<Reservoir> GetAllDams();
    }
}
