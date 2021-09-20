namespace MyFishingApp.Services.Data.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Models;

    public interface IPostsService
    {
        Task<int> CreateAsync(string title, string content, string fishUserId);

        Task DeleteAsync(int postId);

        Post GetById(int id);
    }
}
