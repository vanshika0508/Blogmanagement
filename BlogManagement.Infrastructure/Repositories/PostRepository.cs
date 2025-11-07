using BlogManagement.Core.Entities;
using BlogManagement.Core.Interfaces;
using BlogManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;
        public PostRepository(AppDbContext context) => _context = context;

        public Task<IEnumerable<Post>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Post?> GetByIdAsync(int id, bool includeComments = true)
        {
            IQueryable<Post> q = _context.Posts.AsQueryable();
            if (includeComments) q = q.Include(p => p.Comments);
            return await q.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<Post> AddAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

    
        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Post post)
        {
            throw new NotImplementedException();
        }
    } 
}