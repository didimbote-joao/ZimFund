using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;

namespace ZimFund.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Category> Categories { get; set; } = new();
        public List<ProjectViewModel> Projects { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Buscar categorias não deletadas
            Categories = await _context.Categories
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            // Buscar projetos não deletados e não completados
            Projects = await _context.Projects
                .Include(p => p.Donations)
                .Where(p => !p.IsDeleted)
                .Select(p => new ProjectViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Image = p.Image,
                    CollectedAmount = p.CollectedAmount,
                    GoalAmount = p.GoalAmount,
                    DonationCount = p.Donations.Count
                })
                .ToListAsync();
        }

        public class ProjectViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string? Image { get; set; }
            public decimal CollectedAmount { get; set; }
            public decimal GoalAmount { get; set; }
            public int DonationCount { get; set; }

            public int Percentage =>
                GoalAmount > 0 ? (int)Math.Round((CollectedAmount / GoalAmount) * 100) : 0;
        }
    }
}
