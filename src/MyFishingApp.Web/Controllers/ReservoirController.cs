﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFishingApp.Data.Models;
using MyFishingApp.Services.Data;
using MyFishingApp.Services.Data.Dam;
using MyFishingApp.Services.Data.InputModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFishingAppReact.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReservoirController : ControllerBase
    {
        private readonly IReservoirService reservoirService;
        public ReservoirController(IReservoirService reservoirService)
        {
            this.reservoirService = reservoirService;
        }

        [HttpGet("getAllReservoirs")]
        public string GetAllReservoirs()
        {
            var reservoirs = this.reservoirService.GetAllReservoirs(1);
            var json = JsonConvert.SerializeObject(reservoirs);
            return json;

        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReservoir(CreateReservoirInputModel createReservoirInputModel)
        {
            await this.reservoirService.CreateReservoir(createReservoirInputModel);

            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteReservoir(string reservoirId)
        {
            await this.reservoirService.DeleteReservoir(reservoirId);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateReservoir(string reservoirId, UpdateReservoirInputModel updateReservoirInputModel)
        {
            await this.reservoirService.UpdateReservoir(updateReservoirInputModel, reservoirId);
            return Ok();
         }

        [HttpGet("getById")]
        public string GetReservoirById(string reservoirId)
        {
           var reservoir =  this.reservoirService.GetById(reservoirId);

            var json = JsonConvert.SerializeObject(reservoir);

            return json;
        }

        [HttpGet("getByName")]
        public string GetReservoirByName(string reservoirName)
        {
            var reservoir = this.reservoirService.GetByName(reservoirName);
            var json = JsonConvert.SerializeObject(reservoir);

            return json;
        }

    }
}
