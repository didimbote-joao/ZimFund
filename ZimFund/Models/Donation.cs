using Microsoft.EntityFrameworkCore;

namespace ZimFund.Models
{
    public class Donation
    {
        public int Id { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
