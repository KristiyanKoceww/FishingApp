using Microsoft.AspNetCore.Mvc;
using MyFishingApp.Services.Data.Comments;
using MyFishingApp.Services.Data.InputModels.CommentsInputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFishingApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateComment(CommentsInputModel commentsInputModel)
        {
            await this.commentsService.CreateAsync(commentsInputModel);

            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await this.commentsService.DeleteAsync(commentId);

            return Ok();
        }
    }
}
