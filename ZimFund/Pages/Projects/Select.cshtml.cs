using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZimFund.Data;
using Microsoft.EntityFrameworkCore;
using ZimFund.Models;

namespace ZimFund.Pages.Projects
{
    public class SelectModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SelectModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _context.Projects
                .Include(p => p.User)
                .Include(p => p.Comments).ThenInclude(c => c.User)
                .Include(p => p.Donations).ThenInclude(d => d.User)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (Project == null)
            {
                return NotFound();
            }

            return Page();
        }
    }

}
