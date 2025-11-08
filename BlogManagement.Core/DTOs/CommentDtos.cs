using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Core.DTOs
{
    public class CommentReadDto
    {
        public int Id { get; set; }


        public int PostId { get; set; }

        public string Name { get; set; } = "";

        public string Email { get; set; } = "";

        public string Content { get; set; } = "";



        public DateTime CreatedDate { get; set; }

    }

    public class CommentCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [Required]
        [MaxLength(100)]

        public string Email { get; set; } = "";
        [Required]
        [MaxLength(5000)]

        public string Content { get; set; } = "";



    }

    public class CommentUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [Required]
        [MaxLength(100)]

        public string Email { get; set; } = "";
        [Required]
        [MaxLength(5000)]

        public string Content { get; set; } = "";



        
    }
    

}