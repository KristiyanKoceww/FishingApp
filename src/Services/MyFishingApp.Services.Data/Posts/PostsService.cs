namespace MyFishingApp.Services.Data.Posts
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.PostInputModels;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> appUsersRepository;

        public PostsService(
            IDeletableEntityRepository<Post> postsRepository,
            IDeletableEntityRepository<ApplicationUser> appUsersRepository)
        {
            this.postsRepository = postsRepository;
            this.appUsersRepository = appUsersRepository;
        }

        public static Cloudinary Cloudinary()
        {
            Account account = new();
            Cloudinary cloudinary = new(account);
            cloudinary.Api.Secure = true;

            return cloudinary;
        }

        public async Task<Post> CreateAsync(CreatePostInputModel createPostInputModel)
        {
            var user = this.appUsersRepository.All().Where(x => x.Id == createPostInputModel.UserId).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("No user found by this id.");
            }

            var post = new Post
            {
                Content = createPostInputModel.Content,
                Title = createPostInputModel.Title,
                UserId = createPostInputModel.UserId,
                User = user,
            };

            if (createPostInputModel.FormFiles != null)
            {
                var cloudinary = Cloudinary();
                foreach (var image in createPostInputModel.FormFiles)
                {
                    byte[] bytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        image.CopyTo(memoryStream);
                        bytes = memoryStream.ToArray();
                    }

                    string base64 = Convert.ToBase64String(bytes);

                    var prefix = @"data:image/png;base64,";
                    var imagePath = prefix + base64;

                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(imagePath),
                        Folder = "FishApp/PostImages/",
                    };

                    var uploadResult = await cloudinary.UploadAsync(@uploadParams);

                    var error = uploadResult.Error;

                    if (error != null)
                    {
                        throw new Exception($"Error: {error.Message}");
                    }

                    var imageUrl = new ImageUrls()
                    {
                        ImageUrl = uploadResult.SecureUrl.AbsoluteUri,
                    };

                    post.ImageUrls.Add(imageUrl);
                }
            }

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();
            return post;
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
            var post = this.GetById(postId);
            var comments = post.Comments.Select(x => new Comment()
            {
                Id = x.Id,
                Content = x.Content,
                User = x.User,
                UserId = x.UserId,
                PostId = x.PostId,
                Post = x.Post,
                CreatedOn = x.CreatedOn,
                Parent = x.Parent,
                ParentId = x.ParentId,
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
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                CreatedOn = x.CreatedOn,
                Comments = x.Comments.Select(x => new Comment
                {
                    Id = x.Id,
                    Content = x.Content,
                    User = x.User,
                    UserId = x.UserId,
                    PostId = x.PostId,
                    CreatedOn = x.CreatedOn,
                    Parent = x.Parent,
                    ParentId = x.ParentId,
                }).ToList(),
                ImageUrls = x.ImageUrls.Select(x => new ImageUrls
                {
                    ImageUrl = x.ImageUrl,
                }).ToList(),
                Votes = x.Votes.Select(x => new Vote
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    User = x.User,
                    UserId = x.UserId,
                    Type = x.Type,
                }).ToList(),
                UserId = x.UserId,
                User = x.User,
            }).ToList();
            return posts;
        }

        public IEnumerable<Post> GetPosts(int pageNumber, int pageSize)
        {
            var posts = this.postsRepository.All().OrderByDescending(x => x.CreatedOn).Skip(pageSize * pageNumber).Take(pageSize).Select(x => new Post
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                CreatedOn = x.CreatedOn,
                Comments = x.Comments.Select(x => new Comment
                {
                    Id = x.Id,
                    Content = x.Content,
                    User = x.User,
                    UserId = x.UserId,
                    PostId = x.PostId,
                    CreatedOn = x.CreatedOn,
                    Parent = x.Parent,
                    ParentId = x.ParentId,
                }).ToList(),
                ImageUrls = x.ImageUrls.Select(x => new ImageUrls
                {
                    ImageUrl = x.ImageUrl,
                }).ToList(),
                Votes = x.Votes.Select(x => new Vote
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    User = x.User,
                    UserId = x.UserId,
                    Type = x.Type,
                }).ToList(),
                UserId = x.UserId,
                User = x.User,
            }).ToList();

            return posts;
        }

        public Post GetById(int id)
        {
            var post = this.postsRepository.All().Where(x => x.Id == id).Select(x => new Post
            {
                Title = x.Title,
                Content = x.Content,
                Comments = x.Comments.Select(x => new Comment
                {
                    Id = x.Id,
                    Content = x.Content,
                    User = x.User,
                    UserId = x.UserId,
                    PostId = x.PostId,
                    CreatedOn = x.CreatedOn,
                    Parent = x.Parent,
                    ParentId = x.ParentId,
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
            }).FirstOrDefault();
            if (post is not null)
            {
                return post;
            }
            else
            {
                throw new Exception("No post found by this id");
            }
        }

        public async Task UpdateAsync(UpdatePostInputModel updatePostInputModel)
        {
            var post = this.postsRepository.All().Where(x => x.Id == updatePostInputModel.PostId).FirstOrDefault();
            if (post is not null)
            {
                post.Content = updatePostInputModel.Content;
                post.Title = updatePostInputModel.Title;

                if (updatePostInputModel.FormFiles.Count > 0)
                {
                    var cloudinary = Cloudinary();
                    foreach (var image in updatePostInputModel.FormFiles)
                    {
                        byte[] bytes;
                        using (var memoryStream = new MemoryStream())
                        {
                            image.CopyTo(memoryStream);
                            bytes = memoryStream.ToArray();
                        }

                        string base64 = Convert.ToBase64String(bytes);

                        var prefix = @"data:image/png;base64,";
                        var imagePath = prefix + base64;

                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(imagePath),
                            Folder = "FishApp/PostImages/",
                        };

                        var uploadResult = await cloudinary.UploadAsync(@uploadParams);

                        var error = uploadResult.Error;

                        if (error != null)
                        {
                            throw new Exception($"Error: {error.Message}");
                        }

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
