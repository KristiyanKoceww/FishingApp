namespace MyFishingApp.Services.Data.Dam

{
    using System.Collections.Generic;
    using System.Linq;

    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;

    public class ReservoirService : IReservoirService
    {
        private readonly IDeletableEntityRepository<Reservoir> reservoirRepository;

        public ReservoirService(IDeletableEntityRepository<Reservoir> reservoirRepository)
        {
            this.reservoirRepository = reservoirRepository;
        }

        public IEnumerable<Reservoir> GetAllDams()
        {
            //var dams = this.reservoirRepository.All().Select(x => new Reservoir
            //{
            //    x.Name,
            //    x.Type,
            //    x.Description,
            //    x.Latitude,
            //    x.Longitude,
            //}).ToArray();

            var dam = new Reservoir()
            {
                Name = "Dqkovo",
                Type = "golqm",
                Description = "Dqkovo",
                Latitude = 13513513,
                Longitude = 1535325,
            };

            var dam2 = new Reservoir()
            {
                Name = "Dqkovo2222",
                Type = "golqm",
                Description = "Dqkovo",
                Latitude = 135525353513,
                Longitude = 15355235235325,
            };

            var list = new List<Reservoir>();
            list.Add(dam);
            list.Add(dam2);

            return list;
        }
    }
}
