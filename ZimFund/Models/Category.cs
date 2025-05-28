using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ZimFund.Models
{
    public class Category
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "A categoria é obrigatória")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        [ValidateNever]
        public string UserId { get; set; }
        [ValidateNever]
        public ApplicationUser User { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
