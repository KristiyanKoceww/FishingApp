using Microsoft.AspNetCore.Mvc;
using MyFishingApp.Services.Data.InputModels.PostInputModels;
using MyFishingApp.Services.Data.Posts;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MyFishingApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService postsService;

        public PostsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePost(CreatePostInputModel createPostInputModel)
        {
            await this.postsService.CreateAsync(createPostInputModel);

            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            await this.postsService.DeleteAsync(postId);

            return Ok();
        }

        [HttpGet("getPostById/Id")]
        public string GetPostById(int postId)
        {
            var post = this.postsService.GetById(postId);

            var json = JsonConvert.SerializeObject(post);

            return json;
        }
    }
}
