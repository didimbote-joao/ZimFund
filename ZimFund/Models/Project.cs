using Microsoft.EntityFrameworkCore;

namespace ZimFund.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Precision(18, 2)]
        public decimal GoalAmount { get; set; }
        [Precision(18, 2)]
        public decimal CollectedAmount { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}
