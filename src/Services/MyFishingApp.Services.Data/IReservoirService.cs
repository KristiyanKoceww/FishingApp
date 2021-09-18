namespace MyFishingApp.Services.Data
{
    using System.Collections.Generic;

    using MyFishingApp.Data.Models;

    public interface IReservoirService
    {
        IEnumerable<Reservoir> GetAllDams();
    }
}
