namespace MyFishingApp.Services.Data.Knots
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;

    public interface IKnotService
    {
        Task CreateKnot(KnotInputModel knotInputModel);

        Task DeleteKnot(string knotId);

        Task UpdateKnot(KnotInputModel knotInputModel, string knotId);

        Knot GetById(string knotId);

        IEnumerable<Knot> GetAllKnots(int page, int itemsPerPage = 12);
    }
}
