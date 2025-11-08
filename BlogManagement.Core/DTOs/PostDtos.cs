namespace BlogManagement.Core.DTOs
{
    public class PostReadDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";
       
        public string Content { get; set; } ="";

        public string Author { get; set; } = "admin";

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public List<CommentReadDto> Comments { get; set; } = new();

    }
    
}