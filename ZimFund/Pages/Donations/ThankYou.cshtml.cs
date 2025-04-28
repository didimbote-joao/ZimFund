using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZimFund.Data;

namespace ZimFund.Pages.Donations
{
    public class ThankYouModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ThankYouModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string ProjectName { get; set; }
        public decimal AmountDoado { get; set; }

        public async Task<IActionResult> OnGetAsync(int projectId, decimal amount)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            ProjectName = project.Title;
            AmountDoado = amount;

            return Page();
        }
    }
}
