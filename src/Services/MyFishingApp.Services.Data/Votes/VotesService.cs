namespace MyFishingApp.Services.Data.Votes
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyFishingApp.Data.Common.Repositories;
    using MyFishingApp.Data.Models;
    using MyFishingApp.Services.Data.InputModels.VoteInputModels;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;
        private readonly IRepository<Post> postRepository;

        public VotesService(
            IRepository<Vote> votesRepository,
            IRepository<Post> postRepository)
        {
            this.votesRepository = votesRepository;
            this.postRepository = postRepository;
        }

        public int GetVotes(int postId)
        {
            var votes = this.votesRepository.All()
                .Where(x => x.PostId == postId).Select(x => x.Type).ToList();

            var countOfPositiveVotes = 0;
            foreach (var vote in votes)
            {
                if (vote == VoteType.UpVote)
                {
                    countOfPositiveVotes++;
                }
            }

            return countOfPositiveVotes;
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
