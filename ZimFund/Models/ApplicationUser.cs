using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ZimFund.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "O nome completo é obrigatória")]
        public string FullName { get; set; } = "";
        [Required(ErrorMessage = "O endereço é obrigatório")]
        public string Address { get; set; } = "";
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
