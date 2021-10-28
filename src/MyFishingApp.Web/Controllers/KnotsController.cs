using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFishingApp.Services.Data.InputModels;
using MyFishingApp.Services.Data.InputModels.KnotInputModels;
using MyFishingApp.Services.Data.Knots;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFishingApp.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class KnotsController : ControllerBase
    {

        private readonly IKnotService knotService;

        public KnotsController(IKnotService knotService)
        {
            this.knotService = knotService;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateKnot([FromForm] KnotInputModel knotInputModel)
        {
            try
            {
                await this.knotService.CreateKnotAsync(knotInputModel);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteKnot(string knotId)
        {
            await this.knotService.DeleteKnotAsync(knotId);

            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateKnot([FromForm] UpdateKnotInputModel knotInputModel)
        {
            try
            {
                await this.knotService.UpdateKnotAsync(knotInputModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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
            var knot = this.knotService.GetByName(knotName);

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

