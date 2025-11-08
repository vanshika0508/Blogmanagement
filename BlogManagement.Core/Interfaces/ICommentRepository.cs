using BlogManagement.Core.Entities;

namespace BlogManagement.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<IEnumerable<Comment>> GetByPostIdAsync(int postId);
        Task<Comment?> GetByIdAsync(int id);

        Task<Comment> AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);

        Task DeleteAsync(Comment comment);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}