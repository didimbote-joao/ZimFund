using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;
using System.Security.Claims;

namespace ZimFund.Pages.Donations
{
    [Authorize]
    public class SelectModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SelectModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Donation> Donations { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            Donations = await _context.Donations
                .Include(d => d.Project)
                .Where(d => !d.IsDeleted && d.UserId == userId)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }
    }
}
