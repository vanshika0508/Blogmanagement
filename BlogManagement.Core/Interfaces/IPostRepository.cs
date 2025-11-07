using BlogManagement.Core.Entities;

namespace BlogManagement.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(int id, bool includeComments = true);

        Task<Post> AddAsync(Post post);
        Task UpdateAsync(Post post);

        Task DeleteAsync(Post post);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
    
}