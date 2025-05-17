using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Models;

namespace ZimFund.Pages.Users
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public List<ApplicationUser> Users { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public async Task OnGetAsync()
        {
            var query = _userManager.Users
                .Where(u => !u.IsDeleted)
                .Include(u => u.Donations)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(u =>
                    u.FullName.Contains(Search) ||
                    u.Email.Contains(Search));
            }

            Users = await query.OrderBy(u => u.FullName).ToListAsync();
        }
    }
}
