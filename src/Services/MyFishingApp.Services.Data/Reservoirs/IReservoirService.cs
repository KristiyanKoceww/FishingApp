namespace MyFishingApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;

    public interface IReservoirService
    {
        Task CreateReservoir(CreateReservoirInputModel createReservoirInputModel);

        Task DeleteReservoir(string reservoirId);

        Task UpdateReservoir(UpdateReservoirInputModel updateReservoirInputModel);

        Reservoir GetById(string reservoirId);

        Reservoir GetByName(string reservoirName);

        IEnumerable<Reservoir> GetAllReservoirs();
    }
}
