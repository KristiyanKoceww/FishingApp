using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using MyFishingApp.Services.Data.Comments;
using MyFishingApp.Services.Data.InputModels.CommentsInputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace MyFishingApp.Web.Controllers
{
    [Authorize]
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

            var parentId =
                commentsInputModel.ParentId == 0 ?
                    (int?)null :
                    commentsInputModel.ParentId;

            if (parentId.HasValue)
            {
                if (!this.commentsService.IsInPostId(parentId.Value, commentsInputModel.PostId))
                {
                    return this.BadRequest();
                }
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.commentsService.CreateAsync(commentsInputModel.PostId, userId, commentsInputModel.Content,commentsInputModel.ParentId);

            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateComment(int commentId,CommentsInputModel commentsInputModel)
        {
            await this.commentsService.UpdateAsync(commentId,commentsInputModel);

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
