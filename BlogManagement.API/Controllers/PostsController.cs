
using BlogManagement.Core.DTOs;           
using BlogManagement.Core.Entities;      
using BlogManagement.Core.Interfaces;     
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _posts;
        public PostsController(IPostRepository posts) => _posts = posts;

    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostReadDto>>> GetAll()
        {
            var entities = await _posts.GetAllAsync();
            return Ok(entities.Select(MapToReadDto).ToList());
        }

        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PostReadDto>> GetById(int id)
        {
            var post = await _posts.GetByIdAsync(id, includeComments: true);
            if (post is null) return NotFound();
            return Ok(MapToReadDto(post));
        }

        
        [HttpPost]
        public async Task<ActionResult<PostReadDto>> Create(PostCreateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                Author = "admin"   
            };

            await _posts.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, MapToReadDto(entity));
        }

        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, postUpdateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = await _posts.GetByIdAsync(id, includeComments: false);
            if (entity is null) return NotFound();

            entity.Title = dto.Title;
            entity.Content = dto.Content;
            entity.UpdatedDate = System.DateTime.UtcNow;

            await _posts.UpdateAsync(entity);
            return Ok(MapToReadDto(entity));
        }

        
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<postUpdateDto> patchDoc)
        {
            if (patchDoc is null) return BadRequest();

            var entity = await _posts.GetByIdAsync(id, includeComments: false);
            if (entity is null) return NotFound();

            var dto = new postUpdateDto { Title = entity.Title, Content = entity.Content };

            patchDoc.ApplyTo(dto, ModelState);
            if (!TryValidateModel(dto)) return ValidationProblem(ModelState);

            entity.Title = dto.Title;
            entity.Content = dto.Content;
            entity.UpdatedDate = System.DateTime.UtcNow;

            await _posts.UpdateAsync(entity);
            return Ok(MapToReadDto(entity));
        }

        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _posts.GetByIdAsync(id, includeComments: false);
            if (entity is null) return NotFound();

            await _posts.DeleteAsync(entity);  
            return NoContent();
        }

        
        private static PostReadDto MapToReadDto(Post p) => new()
        {
            Id = p.Id,
            Title = p.Title,
            Content = p.Content,
            Author = p.Author,
            CreatedDate = p.CreatedDate,
            UpdatedDate = p.UpdatedDate,
            Comments = p.Comments.Select(c => new CommentReadDto
            {
                Id = c.Id,
                PostId = c.PostId,
                Name = c.Name,
                Email = c.Email,
                Content = c.Content,
                CreatedDate = c.CreatedDate
            }).ToList()
        };
    }
}
