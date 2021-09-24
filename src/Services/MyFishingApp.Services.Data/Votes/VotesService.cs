﻿namespace MyFishingApp.Services.Data.Votes
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.VoteInputModels;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public int GetVotes(int postId)
        {
            var votes = this.votesRepository.All()
                .Where(x => x.PostId == postId).Sum(x => (int)x.Type);
            return votes;
        }

        public async Task VoteAsync(VoteInputModel voteInputModel)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(x => x.PostId == voteInputModel.PostId && x.UserId == voteInputModel.UserId);
            if (vote != null)
            {
                vote.Type = voteInputModel.IsUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                vote = new Vote
                {
                    PostId = voteInputModel.PostId,
                    UserId = voteInputModel.UserId,
                    Type = voteInputModel.IsUpVote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
