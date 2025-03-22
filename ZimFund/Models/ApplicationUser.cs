using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ZimFund.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Nome completo")]
        public string FullName { get; set; } = "";
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
