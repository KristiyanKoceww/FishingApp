﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFishingApp.Services.Data.FishServ;
using MyFishingApp.Services.Data.InputModels.FishInputModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFishingApp.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FishController : ControllerBase
    {

        private readonly IFishService fishService;

        public FishController(IFishService fishService)
        {
            this.fishService = fishService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFish([FromForm] FishInputModel fishInputModel)
        {
            try
            {
                await this.fishService.CreateAsync(fishInputModel);

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteFish(string fishId)
        {
            await this.fishService.DeleteFish(fishId);

            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateFish([FromForm] UpdateFishInputModel fishInputModel)
        {
            try
            {
                await this.fishService.UpdateFish(fishInputModel);
                return Ok();
            }
            catch (Exception ex)
            { 
                return BadRequest(new { message = ex.Message }); 
            }
        }

        [HttpGet("getFishById")]
        public string GetFishById(string fishId)
        {
            var fish = this.fishService.GetById(fishId);

            var json = JsonConvert.SerializeObject(fish);

            return json;
        }

        [HttpGet("getFishByName")]
        public string GetFishByName(string fishName)
        {
            var fish = this.fishService.GetByName(fishName);

            var json = JsonConvert.SerializeObject(fish);

            return json;
        }

        [HttpGet("getAllFish")]
        public string GetAllFish()
        {
            var fish = this.fishService.GetAllFish();

            var json = JsonConvert.SerializeObject(fish);

            return json;
        }
    }
}
