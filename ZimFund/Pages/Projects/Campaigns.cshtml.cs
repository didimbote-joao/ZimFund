using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;

namespace ZimFund.Pages.Projects
{
    public class CampaignsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CampaignsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Category> Categories { get; set; } = new();
        public List<ProjectViewModel> Projects { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public int? SelectedCategoryId => CategoryId;

        public async Task OnGetAsync()
        {
            Categories = await _context.Categories
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            var query = _context.Projects
                .Include(p => p.Donations)
                .Where(p => !p.IsDeleted);

            if (CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == CategoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(p => p.Title.Contains(Search));
            }

            Projects = await query.Select(p => new ProjectViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Image = p.Image,
                CollectedAmount = p.CollectedAmount,
                GoalAmount = p.GoalAmount,
                DonationCount = p.Donations.Count
            }).ToListAsync();
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
