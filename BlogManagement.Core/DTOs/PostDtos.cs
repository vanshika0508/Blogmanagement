using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Core.DTOs
{
    public class PostReadDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Content { get; set; } = "";

        public string Author { get; set; } = "admin";

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public List<CommentReadDto> Comments { get; set; } = new();

    }

    public class PostCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = "";
        [Required]

        public string Content { get; set; } = "";

    }
    
    public class postUpdateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = "";
        [Required]

        public string Content { get; set; } = "";

        
    }
    
}