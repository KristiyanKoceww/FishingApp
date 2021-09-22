namespace MyFishingApp.Services.Data.Comments
{
    using System.Threading.Tasks;

    using MyFishingApp.Services.Data.InputModels.CommentsInputModels;

    public interface ICommentsService
    {
        Task CreateAsync(CommentsInputModel commentsInputModel);

        Task DeleteAsync(int commentId);

        bool IsInPostId(int commentId, int postId);
    }
}
