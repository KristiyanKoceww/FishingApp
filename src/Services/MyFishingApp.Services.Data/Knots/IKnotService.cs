namespace MyFishingApp.Services.Data.Knots
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;

    public interface IKnotService
    {
        Task CreateKnotAsync(KnotInputModel knotInputModel);

        Task DeleteKnotAsync(string knotId);

        Task UpdateKnotAsync(KnotInputModel knotInputModel, string knotId);

        Knot GetById(string knotId);

        IEnumerable<Knot> GetAllKnots();
    }
}
