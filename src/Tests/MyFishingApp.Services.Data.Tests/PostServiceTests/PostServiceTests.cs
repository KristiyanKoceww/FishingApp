﻿namespace MyFishingApp.Services.Data.Tests.PostServiceTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyFishingApp.Data;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Data.Repositories;
    using MyFishingApp.Services.Data.InputModels.PostInputModels;
    using MyFishingApp.Services.Data.Posts;
    using MyFishingApp.Services.Mapping;
    using Xunit;

    public class PostServiceTests
    {
        [Fact]
        public void TestGetPostById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Post { Id = 1, Title = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var postService = new PostsService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var post = postService.GetById(1);

            Assert.Equal("test", post.Title);
            Assert.Equal(1, post.Id);
        }

        [Fact]
        public void GetPostByIdShouldThrowsExcepitonIfPostDoesntExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var postService = new PostsService(repository);

            Assert.Throws<Exception>(() => postService.GetById(1));
        }

        [Fact]
        public async Task TestCreatePost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));

            var postService = new PostsService(repository);
            var post1 = new CreatePostInputModel()
            {
                Title = "test",
                Content = "test",
                UserId = "1",
            };

            await postService.CreateAsync(post1);

            var count = repository.All().Count();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task TestUserCreateMoreThan1Post()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));

            var postService = new PostsService(repository);
            var post1 = new CreatePostInputModel()
            {
                Title = "test",
                Content = "test",
                UserId = "1",
            };
            var post2 = new CreatePostInputModel()
            {
                Title = "test2",
                Content = "test2",
                UserId = "1",
            };

            await postService.CreateAsync(post1);
            await postService.CreateAsync(post2);

            var count = repository.All().Count();

            Assert.Equal(2, count);
        }

        [Fact]
        public async Task TestDeletePost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Post { Id = 1, Title = "test" }).GetAwaiter().GetResult();
            repository.AddAsync(new Post { Id = 2, Title = "test2" }).GetAwaiter().GetResult();
            await repository.SaveChangesAsync();
            var postService = new PostsService(repository);

            await postService.DeleteAsync(1);

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task TestDeletePostShouldThrowsExceptionWhenNoPostFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));

            await repository.SaveChangesAsync();
            var postService = new PostsService(repository);

            await Assert.ThrowsAsync<Exception>(() => postService.DeleteAsync(1));
        }

        [Fact]
        public async Task TestUpdatePostShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Post { Id = 1, Title = "test" }).GetAwaiter().GetResult();
            await repository.SaveChangesAsync();
            var postService = new PostsService(repository);

            var model = new UpdatePostInputModel()
            {
                Content = "test",
                Title = "new test",
            };

            await postService.UpdateAsync(1, model);
            var post = repository.All().Where(x => x.Id == 1).FirstOrDefault();

            Assert.NotNull(post);
            Assert.Equal("test", post.Content);
            Assert.Equal("new test", post.Title);
        }

        [Fact]
        public async Task TestUpdatePostShouldThrowsExceptionWhenNoPostFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));

            await repository.SaveChangesAsync();
            var postService = new PostsService(repository);

            var model = new UpdatePostInputModel()
            {
                Content = "test",
                Title = "new test",
            };
            await Assert.ThrowsAsync<Exception>(() => postService.UpdateAsync(1, model));
        }

        public class MyTestPost : IMapFrom<Post>
        {
            public string Title { get; set; }
        }

    }
}
