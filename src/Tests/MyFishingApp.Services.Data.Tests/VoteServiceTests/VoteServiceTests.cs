namespace MyFishingApp.Services.Data.Tests.VoteServiceTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyFishingApp.Data;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Data.Repositories;
    using MyFishingApp.Services.Data.InputModels.VoteInputModels;
    using MyFishingApp.Services.Data.Votes;
    using Xunit;

    public class VoteServiceTests
    {
        [Fact]
        public async Task TwoDownVotesShouldCountOnce()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var voteRepository = new EfRepository<Vote>(new ApplicationDbContext(options.Options));
            var postRepository = new EfRepository<Post>(new ApplicationDbContext(options.Options));
            var service = new VotesService(voteRepository, postRepository);

            var input = new VoteInputModel()
            {
                PostId = 1,
                UserId = "1",
                IsUpVote = false,
            };

            var input2 = new VoteInputModel()
            {
                PostId = 1,
                UserId = "2",
                IsUpVote = false,
            };

            for (int i = 0; i < 100; i++)
            {
                await service.VoteAsync(input);
            }

            for (int i = 0; i < 100; i++)
            {
                await service.VoteAsync(input2);
            }

            var votes = service.GetVotes(1);
            Assert.Equal(-2, votes);
        }

        [Fact]
        public async Task TwoUpVotesShouldCountOnce()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var voteRepository = new EfRepository<Vote>(new ApplicationDbContext(options.Options));
            var postRepository = new EfRepository<Post>(new ApplicationDbContext(options.Options));
            var service = new VotesService(voteRepository, postRepository);

            var input = new VoteInputModel()
            {
                PostId = 1,
                UserId = "1",
                IsUpVote = true,
            };

            var input2 = new VoteInputModel()
            {
                PostId = 1,
                UserId = "2",
                IsUpVote = true,
            };

            for (int i = 0; i < 100; i++)
            {
                await service.VoteAsync(input);
            }

            for (int i = 0; i < 100; i++)
            {
                await service.VoteAsync(input2);
            }

            var votes = service.GetVotes(1);
            Assert.Equal(2, votes);
        }

        [Fact]
        public async Task TestGetVotes()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var voteRepository = new EfRepository<Vote>(new ApplicationDbContext(options.Options));
            var postRepository = new EfRepository<Post>(new ApplicationDbContext(options.Options));
            var service = new VotesService(voteRepository, postRepository);

            var input = new VoteInputModel()
            {
                PostId = 1,
                UserId = "1",
                IsUpVote = true,
            };

            var input2 = new VoteInputModel()
            {
                PostId = 1,
                UserId = "2",
                IsUpVote = true,
            };

            await service.VoteAsync(input);
            await service.VoteAsync(input2);

            var voteCount = service.GetVotes(1);

            Assert.Equal(2, voteCount);
        }

        [Fact]
        public async Task TestCreateVote()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var voteRepository = new EfRepository<Vote>(new ApplicationDbContext(options.Options));
            var postRepository = new EfRepository<Post>(new ApplicationDbContext(options.Options));
            var service = new VotesService(voteRepository, postRepository);

            var input = new VoteInputModel()
            {
                PostId = 1,
                UserId = "1",
                IsUpVote = true,
            };

            await service.VoteAsync(input);
            var votes = voteRepository.All().Count();
            Assert.Equal(1, votes);
        }
    }
}
