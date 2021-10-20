namespace MyFishingApp.Services.Data.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
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

            if (createPostInputModel.ImageUrls != null)
            {
                Account account = new();
                Cloudinary cloudinary = new(account);
                cloudinary.Api.Secure = true;
                var count = 0;
                foreach (var image in createPostInputModel.ImageUrls)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription($"{image.ImageUrl}"),
                        PublicId = post.Id.ToString() + count,
                        Folder = "FishApp/PostImages/",
                    };

                    var uploadResult = cloudinary.Upload(uploadParams);
                    count++;

                    var imageUrl = new ImageUrls()
                    {
                        ImageUrl = uploadResult.SecureUrl.AbsoluteUri,
                    };

                    post.ImageUrls.Add(imageUrl);
                }
            }

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
            else
            {
                throw new Exception("No post found by this id");
            }
        }

        public ICollection<Comment> GetAllCommentsToPost(int postId)
        {
            var post = this.postsRepository.All().Where(x => x.Id == postId).FirstOrDefault();
            var comments = post.Comments.Select(x => new Comment()
            {
                Content = x.Content,
                User = x.User,
                UserId = x.UserId,
                PostId = x.PostId,
            }).ToList();

            if (comments.Count == 0)
            {
                throw new Exception("There are no coments for this post!");
            }

            return comments;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            var posts = this.postsRepository.All().Select(x => new Post
            {
                Title = x.Title,
                Content = x.Content,
                Comments = x.Comments.Select(x => new Comment
                {
                    Content = x.Content,
                    User = x.User,
                    UserId = x.UserId,
                    PostId = x.PostId,
                }).ToList(),
                ImageUrls = x.ImageUrls.Select(x => new ImageUrls
                {
                    ImageUrl = x.ImageUrl,
                }).ToList(),
                Votes = x.Votes.Select(x => new Vote
                {
                    Type = x.Type,
                }).ToList(),
                UserId = x.UserId,
                User = x.User,
            }).ToList();
            return posts;
        }

        public Post GetById(int id)
        {
            var post = this.postsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            if (post is not null)
            {
                return post;
            }
            else
            {
                throw new Exception("No post found by this id");
            }
        }

        public async Task UpdateAsync(int postId, UpdatePostInputModel updatePostInputModel)
        {
            var post = this.postsRepository.All().Where(x => x.Id == postId).FirstOrDefault();
            if (post is not null)
            {
                post.Content = updatePostInputModel.Content;
                post.Title = updatePostInputModel.Title;

                if (updatePostInputModel.ImageUrls != null)
                {
                    Account account = new();
                    Cloudinary cloudinary = new(account);
                    cloudinary.Api.Secure = true;
                    var count = 0;
                    foreach (var image in updatePostInputModel.ImageUrls)
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription($"{image.ImageUrl}"),
                            PublicId = post.Id.ToString() + count,
                            Folder = "FishApp/PostImages/",
                        };

                        var uploadResult = cloudinary.Upload(uploadParams);
                        count++;

                        var imageUrl = new ImageUrls()
                        {
                            ImageUrl = uploadResult.SecureUrl.AbsoluteUri,
                        };

                        post.ImageUrls.Add(imageUrl);
                    }
                }

                this.postsRepository.Update(post);
                await this.postsRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No post found by this id");
            }
        }
    }
}
