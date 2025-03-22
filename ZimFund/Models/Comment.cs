namespace ZimFund.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
