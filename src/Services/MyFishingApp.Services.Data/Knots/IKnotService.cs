namespace MyFishingApp.Services.Data.Knots
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;

    public interface IKnotService
    {
        Task CreateReservoir(KnotInputModel knotInputModel);

        Task DeleteReservoir(string reservoirId);

        Task UpdateReservoir(KnotInputModel knotInputModel, string knotId);

        Knot GetById(string reservoirId);

        IEnumerable<Knot> GetAllReservoirs(int page, int itemsPerPage = 12);
    }
}
