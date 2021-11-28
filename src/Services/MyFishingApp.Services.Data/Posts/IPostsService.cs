namespace MyFishingApp.Services.Data.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.PostInputModels;

    public interface IPostsService
    {
        Task<Post> CreateAsync(CreatePostInputModel createPostInputModel);

        Task DeleteAsync(int postId);

        Task UpdateAsync(UpdatePostInputModel updatePostInputModel);

        Post GetById(int id);

        ICollection<Comment> GetAllCommentsToPost(int postId);

        IEnumerable<Post> GetAllPosts();
    }
}
