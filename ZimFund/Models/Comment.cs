namespace ZimFund.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
