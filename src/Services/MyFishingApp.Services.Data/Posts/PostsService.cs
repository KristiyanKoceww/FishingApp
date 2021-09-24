namespace MyFishingApp.Services.Data.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.PostInputModels;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public async Task<int> CreateAsync(CreatePostInputModel createPostInputModel)
        {
            var post = new Post
            {
                Content = createPostInputModel.Content,
                Title = createPostInputModel.Title,
                UserId = createPostInputModel.UserId,
            };

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();
            return post.Id;
        }

        public async Task DeleteAsync(int postId)
        {
            var post = this.postsRepository.All().Where(x => x.Id == postId).FirstOrDefault();

            if (post is not null)
            {
               this.postsRepository.Delete(post);
               await this.postsRepository.SaveChangesAsync();
            }
        }

        public Post GetById(int id)
        {
            var post = this.postsRepository.All().Where(x => x.Id == id).FirstOrDefault();

            return post;
        }
    }
}
