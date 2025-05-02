using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ZimFund.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
        [ValidateNever]
        public ApplicationUser User { get; set; }
        [ValidateNever]
        public int ProjectId { get; set; }
        [ValidateNever]
        public Project Project { get; set; }
    }
}
