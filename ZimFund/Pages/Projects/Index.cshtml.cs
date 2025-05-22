using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;

namespace ZimFund.Pages.Projects
{
    [Authorize(Roles = "admin,organizer")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Project> Projects { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public async Task OnGetAsync(int page = 1)
        {
            //Projects = await _context.Projects
            //    .Include(p => p.Category)
            //    .Include(p => p.User)
            //    .Where(p => !p.IsDeleted)
            //    .OrderByDescending(p => p.CreatedAt)
            //    .ToListAsync();

            //var user = await _userManager.GetUserAsync(User);
            //var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            //Projects = await _context.Projects
            //    .Where(p => !p.IsDeleted && (isAdmin || p.UserId == user.Id))
            //    .Include(p => p.User)
            //    .OrderByDescending(p => p.CreatedAt)
            //    .ToListAsync();

            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "admin");

            var query = _context.Projects
                .Where(p => !p.IsDeleted && (isAdmin || p.UserId == user.Id))
                .Include(p => p.User)
                .Include(p => p.Category) 
                .OrderByDescending(p => p.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                query = query.Where(p => p.Title.Contains(SearchTerm));
            }

            Projects = await query.ToListAsync();
        }
    }
}