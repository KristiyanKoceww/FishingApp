namespace MyFishingApp.Services.Data.Votes
{
    using MyFishingApp.Services.Data.InputModels.VoteInputModels;
    using System.Threading.Tasks;

    public interface IVotesService
    {
        /// <summary>
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <param name="isUpVote">If true - up vote, else - down vote.</param>
        /// <returns></returns>
        Task VoteAsync(VoteInputModel voteInputModel);

        int GetVotes(int postId);
    }
}
