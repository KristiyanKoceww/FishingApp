namespace MyFishingApp.Services.Data.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.CommentsInputModels;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;
        private readonly IDeletableEntityRepository<Post> postRepository;

        public CommentsService(
            IDeletableEntityRepository<Comment> commentsRepository,
            IDeletableEntityRepository<Post> postRepository)
        {
            this.commentsRepository = commentsRepository;
            this.postRepository = postRepository;
        }

        //public async Task CreateAsync(int postId, string userId, string content, int? parentId = null)
        //{
        //    var post = this.postRepository.All().Where(x => x.Id == postId).FirstOrDefault();
        //    if (post is null)
        //    {
        //        throw new Exception("There is no post found by this id!");
        //    }

        //    var comment = new Comment
        //    {
        //        Content = content,
        //        ParentId = parentId,
        //        PostId = postId,
        //        UserId = userId,
        //    };

        //    post.Comments.Add(comment);

        //    this.postRepository.Update(post);
        //    await this.commentsRepository.AddAsync(comment);
        //    await this.commentsRepository.SaveChangesAsync();
        //}

        public async Task CreateAsync(CommentsInputModel commentsInputModel)
        {
            var comment = new Comment
            {
                Content = commentsInputModel.Content,
                PostId = commentsInputModel.PostId,
                UserId = commentsInputModel.UserId,
            };

            if (commentsInputModel.ParentId is not null)
            {
                comment.ParentId = commentsInputModel.ParentId;
            }

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int commentId)
        {
            var comment = this.commentsRepository.All().Where(x => x.Id == commentId).FirstOrDefault();
            if (comment is not null)
            {
                this.commentsRepository.Delete(comment);
                await this.commentsRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No comment found  by this id");
            }
        }

        public bool IsInPostId(int commentId, int postId)
        {
            var commentPostId = this.commentsRepository.All().Where(x => x.Id == commentId)
                .Select(x => x.PostId).FirstOrDefault();
            return commentPostId == postId;
        }

        public async Task UpdateAsync(int commentId, CommentsInputModel commentsInputModel)
        {
            var post = this.postRepository.All().Where(x => x.Id == commentsInputModel.PostId).FirstOrDefault();
            if (post is null)
            {
                throw new Exception("No post found  by this id");
            }

            var comment = post.Comments.FirstOrDefault(x => x.Id == commentId);

            // var comment = this.commentsRepository.All().Where(x => x.Id == commentId).FirstOrDefault();
            if (comment is not null)
            {
                comment.Content = commentsInputModel.Content;
                this.commentsRepository.Update(comment);
                await this.commentsRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No comment found  by this id");
            }
        }
    }
}
