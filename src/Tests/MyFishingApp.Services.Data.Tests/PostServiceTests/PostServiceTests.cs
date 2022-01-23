namespace MyFishingApp.Services.Data.Tests.PostServiceTests
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
        public async Task TestGetPostById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options2 = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var postsRepository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var appUsersRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options2.Options));

            var postService = new PostsService(postsRepository, appUsersRepository);

            await appUsersRepository.AddAsync(new ApplicationUser()
            {
                Id = "1",
                FirstName = "user",
                LastName = "user",
                Age = 20,
                Email = "user@gmail.com",
                Gender = Gender.Female,
                UserName = "user",
            });

            await appUsersRepository.SaveChangesAsync();
            var user = appUsersRepository.All().FirstOrDefault();

            await postsRepository.AddAsync(new Post { Id = 50, Title = "test", User = user, UserId = user.Id, });

            await postsRepository.SaveChangesAsync();

            var post = postService.GetById(50);

            Assert.Equal("test", post.Title);
            Assert.Equal("user", post.User.FirstName);
            Assert.Equal(50, post.Id);
        }

        [Fact]
        public void GetPostByIdShouldThrowsExcepitonIfPostDoesntExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var postsRepository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var appUsersRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var postService = new PostsService(postsRepository, appUsersRepository);

            Assert.Throws<Exception>(() => postService.GetById(1));
        }

        [Fact]
        public async Task TestCreatePost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var options2 = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());


            var postsRepository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var appUsersRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options2.Options));

            var postService = new PostsService(postsRepository, appUsersRepository);

            await appUsersRepository.AddAsync(new ApplicationUser()
            {
                Id = "1",
                FirstName = "user",
                LastName = "user",
                Age = 20,
                Email = "user@gmail.com",
                Gender = Gender.Female,
                UserName = "user",
            });

            await appUsersRepository.SaveChangesAsync();
            var post1 = new CreatePostInputModel()
            {
                Title = "test",
                Content = "test",
                UserId = "1",
            };

            await postService.CreateAsync(post1);

            var count = postsRepository.All().Count();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task TestUserCreateMoreThan1Post()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options2 = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var postsRepository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var appUsersRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options2.Options));
            var postService = new PostsService(postsRepository, appUsersRepository);

            await appUsersRepository.AddAsync(new ApplicationUser()
            {
                Id = "1",
                FirstName = "user",
                LastName = "user",
                Age = 20,
                Email = "user@gmail.com",
                Gender = Gender.Female,
                UserName = "user",
            });

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

            await appUsersRepository.SaveChangesAsync();
            await postService.CreateAsync(post1);
            await postService.CreateAsync(post2);

            var count = postsRepository.All().Count();

            Assert.Equal(2, count);
        }

        [Fact]
        public async Task TestDeletePost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var postsRepository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var appUsersRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            await postsRepository.AddAsync(new Post { Id = 1, Title = "test" });
            await postsRepository.AddAsync(new Post { Id = 2, Title = "test2" });
            await postsRepository.SaveChangesAsync();
            var postService = new PostsService(postsRepository, appUsersRepository);

            await postService.DeleteAsync(1);

            Assert.Equal(1, postsRepository.All().Count());
        }

        [Fact]
        public async Task TestDeletePostShouldThrowsExceptionWhenNoPostFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var postsRepository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var appUsersRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            await postsRepository.SaveChangesAsync();
            var postService = new PostsService(postsRepository, appUsersRepository);

            await Assert.ThrowsAsync<Exception>(() => postService.DeleteAsync(1));
        }

        [Fact]
        public async Task TestUpdatePostShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var postsRepository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var appUsersRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            await postsRepository.AddAsync(new Post { Id = 1, Title = "test" });
            await postsRepository.SaveChangesAsync();
            var postService = new PostsService(postsRepository, appUsersRepository);

            var model = new UpdatePostInputModel()
            {
                Content = "test",
                Title = "new test",
                PostId = 1,
            };

            await postService.UpdateAsync(model);
            var post = postsRepository.All().Where(x => x.Id == 1).FirstOrDefault();

            Assert.NotNull(post);
            Assert.Equal("test", post.Content);
            Assert.Equal("new test", post.Title);
        }

        [Fact]
        public async Task TestUpdatePostShouldThrowsExceptionWhenNoPostFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var postsRepository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var appUsersRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var postService = new PostsService(postsRepository, appUsersRepository);

            await postsRepository.SaveChangesAsync();

            var model = new UpdatePostInputModel()
            {
                Content = "test",
                Title = "new test",
            };
            await Assert.ThrowsAsync<Exception>(() => postService.UpdateAsync(model));
        }

        public class MyTestPost : IMapFrom<Post>
        {
            public string Title { get; set; }
        }

    }
}
