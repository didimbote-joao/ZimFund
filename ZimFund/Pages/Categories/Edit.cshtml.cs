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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(Category.Name))
                return Page();

            var user = await _userManager.GetUserAsync(User);

            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == Category.Id && c.UserId == user.Id && !c.IsDeleted);

            if (existingCategory == null)
                return NotFound();

            // Verifica se já existe outra categoria com o mesmo nome
            var nameExists = await _context.Categories
                .AnyAsync(c => c.Name == Category.Name && c.Id != Category.Id && c.UserId == user.Id && !c.IsDeleted);

            if (nameExists)
            {
                ModelState.AddModelError(string.Empty, "Já existe uma categoria com esse nome.");
                return Page();
            }

            existingCategory.Name = Category.Name;
            existingCategory.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
