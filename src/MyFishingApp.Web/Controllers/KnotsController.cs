using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFishingApp.Services.Data.InputModels;
using MyFishingApp.Services.Data.Knots;
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
    public class KnotsController : ControllerBase
    {

        private readonly IKnotService knotService;

        public KnotsController(IKnotService knotService)
        {
            this.knotService = knotService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateKnot(KnotInputModel knotInputModel)
        {
            await this.knotService.CreateKnotAsync(knotInputModel);

            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteKnot(string knotId)
        {
            await this.knotService.DeleteKnotAsync(knotId);

            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateKnot(KnotInputModel knotInputModel, string knotId)
        {
            await this.knotService.UpdateKnotAsync(knotInputModel, knotId);

            return Ok();
        }

        [HttpGet("getKnotById")]
        public string GetKnotById(string knotId)
        {
            var knot = this.knotService.GetById(knotId);

            var json = JsonConvert.SerializeObject(knot);

            return json;
        }

        [HttpGet("getKnotByName")]
        public string GetKnotByName(string knotName)
        {
            var knot = this.knotService.GetById(knotName);

            var json = JsonConvert.SerializeObject(knot);

            return json;
        }

        [HttpGet("getAllKnots")]
        public string GetAllKnot()
        {
            var knots = this.knotService.GetAllKnots();

            var json = JsonConvert.SerializeObject(knots);

            return json;
        }
    }
}

