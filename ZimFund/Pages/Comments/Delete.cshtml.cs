using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;

namespace ZimFund.Pages.Comments
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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

            if (Comment == null || Comment.IsDeleted)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var commentInDb = await _context.Comments.FindAsync(Comment.Id);

            if (commentInDb == null || commentInDb.IsDeleted)
                return NotFound();

            commentInDb.IsDeleted = true;
            commentInDb.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Projects/Select", new { id = commentInDb.ProjectId });
        }
    }
}
