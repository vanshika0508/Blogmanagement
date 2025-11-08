
using BlogManagement.Core.DTOs;          
using BlogManagement.Core.Entities;       
using BlogManagement.Core.Interfaces;     
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class CommentsController : ControllerBase
    {
        private readonly IPostRepository _posts;
        private readonly ICommentRepository _comments;

        public CommentsController(IPostRepository posts, ICommentRepository comments)
        {
            _posts = posts;
            _comments = comments;
        }

        
        [HttpGet("comments")]
        public async Task<ActionResult<IEnumerable<CommentReadDto>>> GetAll()
        {
            var items = await _comments.GetAllAsync();
            return Ok(items.Select(MapToReadDto));
        }

        
        [HttpGet("comments/{id:int}")]
        public async Task<ActionResult<CommentReadDto>> GetById(int id)
        {
            var item = await _comments.GetByIdAsync(id);
            if (item is null) return NotFound();
            return Ok(MapToReadDto(item));
        }

        
        [HttpGet("posts/{postId:int}/comments")]
        public async Task<ActionResult<IEnumerable<CommentReadDto>>> GetForPost(int postId)
        {
            if (!await _posts.ExistsAsync(postId)) return NotFound();
            var list = await _comments.GetByPostIdAsync(postId);
            return Ok(list.Select(MapToReadDto));
        }

        
        [HttpPost("posts/{postId:int}/comments")]
        public async Task<ActionResult<CommentReadDto>> CreateForPost(int postId, CommentCreateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var parent = await _posts.GetByIdAsync(postId, includeComments: false);
            if (parent is null) return NotFound();

            var entity = new Comment
            {
                PostId = postId,
                Name = dto.Name,
                Email = dto.Email,
                Content = dto.Content
            };

            await _comments.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, MapToReadDto(entity));
        }

        
        [HttpPut("comments/{id:int}")]
        public async Task<IActionResult> Update(int id, CommentUpdateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = await _comments.GetByIdAsync(id);
            if (entity is null) return NotFound();

            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.Content = dto.Content;

            await _comments.UpdateAsync(entity);
            return Ok(MapToReadDto(entity));
        }

        
        [HttpPatch("comments/{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CommentUpdateDto> patchDoc)
        {
            if (patchDoc is null) return BadRequest();

            var entity = await _comments.GetByIdAsync(id);
            if (entity is null) return NotFound();

            var dto = new CommentUpdateDto
            {
                Name = entity.Name,
                Email = entity.Email,
                Content = entity.Content
            };

            patchDoc.ApplyTo(dto, ModelState);
            if (!TryValidateModel(dto)) return ValidationProblem(ModelState);

            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.Content = dto.Content;

            await _comments.UpdateAsync(entity);
            return Ok(MapToReadDto(entity));
        }

        
        [HttpDelete("comments/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _comments.GetByIdAsync(id);
            if (entity is null) return NotFound();

            await _comments.DeleteAsync(entity);
            return NoContent();
        }

        private static CommentReadDto MapToReadDto(Comment c) => new()
        {
            Id = c.Id,
            PostId = c.PostId,
            Name = c.Name,
            Email = c.Email,
            Content = c.Content,
            CreatedDate = c.CreatedDate
        };
    }
}
