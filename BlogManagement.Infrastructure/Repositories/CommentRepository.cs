using BlogManagement.Core.Entities;
using BlogManagement.Core.Interfaces;
using BlogManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context)
        {
             _context = context;
        }
        public  async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments.AsNoTracking()
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public Task<Comment?> GetByIdAsync(int id)
        {
            return _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetByPostIdAsync(int postId)
        {
            return await _context.Comments.AsNoTracking()
                         .Where(c => c.PostId == postId)
                         .OrderByDescending(c => c.CreatedDate)
                         .ToListAsync();;
        }

        public  async Task<Comment> AddAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task DeleteAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Comments.AnyAsync(c => c.Id == id);
        }

        
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
           
        }
    }
}
        