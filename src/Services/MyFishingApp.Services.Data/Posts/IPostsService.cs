namespace MyFishingApp.Services.Data.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.PostInputModels;

    public interface IPostsService
    {
        Task<int> CreateAsync(CreatePostInputModel createPostInputModel);

        Task DeleteAsync(int postId);

        Post GetById(int id);
    }
}
