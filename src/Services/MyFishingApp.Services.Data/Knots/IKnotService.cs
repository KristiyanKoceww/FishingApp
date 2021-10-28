namespace MyFishingApp.Services.Data.Knots
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels;
    using MyFishingApp.Services.Data.InputModels.KnotInputModels;

    public interface IKnotService
    {
        Task CreateKnotAsync(KnotInputModel knotInputModel);

        Task DeleteKnotAsync(string knotId);

        Task UpdateKnotAsync(UpdateKnotInputModel updateKnotInputModel);

        Knot GetById(string knotId);

        Knot GetByName(string knotName);

        IEnumerable<Knot> GetAllKnots();
    }
}
