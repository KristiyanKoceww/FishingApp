namespace MyFishingApp.Services.Data.Comments
{
    using System.Threading.Tasks;

    using MyFishingApp.Services.Data.InputModels.CommentsInputModels;

    public interface ICommentsService
    {
        //Task CreateAsync(CommentsInputModel commentsInputModel, string userId);
        Task CreateAsync(int postId, string userId, string content, int? parentId = null);

        Task DeleteAsync(int commentId);

        Task UpdateAsync(int commentId, CommentsInputModel commentsInputModel);

        bool IsInPostId(int commentId, int postId);
    }
}
