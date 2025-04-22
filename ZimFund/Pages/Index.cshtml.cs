using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;

namespace ZimFund.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<Category> Categories { get; set; }
        public List<Project> Projects { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
            Projects = await _context.Projects.Include(p => p.Donations).ToListAsync();
        }
    }
}
