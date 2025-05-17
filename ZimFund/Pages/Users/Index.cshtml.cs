using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        public async Task OnGetAsync()
        {
            Users = await _userManager.Users
                .Where(u => !u.IsDeleted)
                .Include(u => u.Donations)
                .OrderBy(u => u.FullName)
                .ToListAsync();
        }
    }
}