using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ZimFund.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O título é obrigatório")]
        public string Title { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Description { get; set; }
        [Required(ErrorMessage = "O valor é obrigatório")]
        [Precision(18, 2)]
        public decimal GoalAmount { get; set; }
        [Precision(18, 2)]
        public decimal CollectedAmount { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
        [ValidateNever]
        public ApplicationUser User { get; set; }
        [Required(ErrorMessage = "A categoria é obrigatória")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}
