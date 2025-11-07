using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Core.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(5000)]
        public string Content { get; set; } = string.Empty;



        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public Post? Post { get; set; }
    }
}