using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFishingApp.Data.Models;
using MyFishingApp.Services.Data;
using MyFishingApp.Services.Data.Dam;
using MyFishingApp.Services.Data.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFishingAppReact.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReservoirController : ControllerBase
    {
        private readonly IReservoirService reservoirService;
        public ReservoirController(IReservoirService reservoirService)
        {
            this.reservoirService = reservoirService;
        }

        [HttpGet]
        public IEnumerable<Reservoir> Get()
        {
             var reservoirs = this.reservoirService.GetAllReservoirs(1);
            return reservoirs;
            
        }

        [HttpPost("create")]
        public void CreateReservoir(CreateReservoirInputModel createReservoirInputModel)
        {
            this.reservoirService.CreateReservoir(createReservoirInputModel);
        }

    }
}
