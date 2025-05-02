using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;

namespace ZimFund.Pages.Comments
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Comment Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Comment = await _context.Comments
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Comment == null )
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var commentInDb = await _context.Comments.FindAsync(Comment.Id);

            if (commentInDb == null )
                return NotFound();

            commentInDb.Content = Comment.Content;
            commentInDb.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Projects/Select", new { id = commentInDb.ProjectId });
        }
    }
}
