namespace MyFishingApp.Services.Data.Comments
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task Create(int postId, string userId, string content, int? parentId = null);

        Task Delete(int commentId);

        bool IsInPostId(int commentId, int postId);
    }
}
