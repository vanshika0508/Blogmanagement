using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BlogManagement.Core.Entities
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]

        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;

        public string Author { get; set; } = "admin";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();






    }
}