namespace ZimFund.Models
{
    public class Category
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
