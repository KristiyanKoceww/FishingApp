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

        Task UpdateReservoir(UpdateReservoirInputModel updateReservoirInputModel, string reservoirId);

        Reservoir GetById(string reservoirId);

        IEnumerable<Reservoir> GetAllReservoirs(int page, int itemsPerPage = 12);
    }
}
