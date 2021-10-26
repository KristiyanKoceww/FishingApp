﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFishingApp.Services.Data.InputModels.PostInputModels;
using MyFishingApp.Services.Data.Posts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<IActionResult> CreatePost([FromForm] CreatePostInputModel createPostInputModel)
        {

            try
            {
                await this.postsService.CreateAsync(createPostInputModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            await this.postsService.DeleteAsync(postId);

            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdatePost(int postId, UpdatePostInputModel updatePostInputModel)
        {
            await this.postsService.UpdateAsync(postId, updatePostInputModel);

            return Ok();
        }



        [HttpGet("getPostById/Id")]
        public string GetPostById(int postId)
        {
            var post = this.postsService.GetById(postId);

            var json = JsonConvert.SerializeObject(post);

            return json;
        }

        [HttpGet("getPostCommentsByPostId/Id")]
        public string GetPostCommentsByPostId(int postId)
        {
            var post = this.postsService.GetAllCommentsToPost(postId);

            var json = JsonConvert.SerializeObject(post);

            return json;
        }

        [HttpGet("getAllPosts")]
        public string GetAllPosts()
        {
            var post = this.postsService.GetAllPosts();

            var json = JsonConvert.SerializeObject(post);

            return json;
        }
    }
}
