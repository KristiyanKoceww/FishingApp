using Microsoft.AspNetCore.Mvc;
using MyFishingApp.Services.Data.InputModels.VoteInputModels;
using MyFishingApp.Services.Data.Votes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFishingApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost("vote")]
        public async Task<IActionResult> Vote(VoteInputModel voteInputModel)
        {
            await this.votesService.VoteAsync(voteInputModel);

            return Ok();
        }

        [HttpGet("getVotes")]
        public int GetVotes(int postId)
        {
            return this.votesService.GetVotes(postId);
        }
    }
}
