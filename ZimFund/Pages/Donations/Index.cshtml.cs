using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;

namespace ZimFund.Pages.Donations
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Donation> Donations { get; set; }
        public List<Project> Projects { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int? ProjectId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public async Task OnGetAsync()
        {
            Projects = await _context.Projects
                .Where(p => !p.IsDeleted)
                .OrderBy(p => p.Title)
                .ToListAsync();

            var query = _context.Donations
                .Where(d => !d.IsDeleted)
                .Include(d => d.User)
                .Include(d => d.Project)
                .AsQueryable();

            if (ProjectId.HasValue)
            {
                query = query.Where(d => d.ProjectId == ProjectId.Value);
            }

            if (StartDate.HasValue)
            {
                query = query.Where(d => d.CreatedAt >= StartDate.Value);
            }

            if (EndDate.HasValue)
            {
                // Considerar o final do dia
                query = query.Where(d => d.CreatedAt <= EndDate.Value.Date.AddDays(1).AddTicks(-1));
            }

            Donations = await query
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }
    }
}
