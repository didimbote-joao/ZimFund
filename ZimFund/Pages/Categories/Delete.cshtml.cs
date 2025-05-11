using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;

namespace ZimFund.Pages.Categories
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            Category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id && !c.IsDeleted);

            if (Category == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == Category.Id && c.UserId == user.Id && !c.IsDeleted);

            if (category == null)
                return NotFound();

            category.IsDeleted = true;
            category.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
