using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ZimFund.Pages.Comments
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Comment> Comments { get; set; } = new List<Comment>();

        [BindProperty(SupportsGet = true)]
        public string UserFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ProjectFilter { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Comments
                .Include(c => c.User)
                .Include(c => c.Project)
                .Where(c => !c.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(UserFilter))
            {
                query = query.Where(c => c.User.FullName.Contains(UserFilter));
            }

            if (!string.IsNullOrEmpty(ProjectFilter))
            {
                query = query.Where(c => c.Project.Title.Contains(ProjectFilter));
            }

            Comments = await query
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
