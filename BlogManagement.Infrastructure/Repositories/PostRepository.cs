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

        public async Task<Post> AddAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task DeleteAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {

            return _context.Posts.AnyAsync(p => p.Id == id);
        }



        public Task SaveChangesAsync()
        {

           return _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
    } 
}