using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;

namespace ZimFund.Pages.Comments
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Comment Comment { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == null)
                return NotFound();

            Project = await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == Id && !p.IsDeleted);

            if (Project == null)
                return NotFound();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Verifica se o projecto e' valido 
            var projectExists = await _context.Projects.AnyAsync(p => p.Id == Comment.ProjectId && !p.IsDeleted);
            if (!projectExists)
            {
                ModelState.AddModelError(string.Empty, "Projeto inválido ou inexistente.");
                return Page();
            }

            Comment.UserId = _userManager.GetUserId(User);
            Comment.CreatedAt = DateTime.Now;
            Comment.UpdatedAt = DateTime.Now;

            _context.Comments.Add(Comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Projects/Select", new { id = Comment.ProjectId });
        }

    }

}
