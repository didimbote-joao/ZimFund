using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZimFund.Data;
using ZimFund.Models;

namespace ZimFund.Pages.Categories
{
    [Authorize(Roles = "admin")]
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
        public Category Category { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(Category.Name))
                return Page();

            var user = await _userManager.GetUserAsync(User);

            // Verifica se o nome já está em uso pelo mesmo usuário nao deletada
            var categoryExists = _context.Categories
                .Any(c => c.Name == Category.Name && c.UserId == user.Id && !c.IsDeleted);

            if (categoryExists)
            {
                ModelState.AddModelError(string.Empty, "Já existe uma categoria com esse nome.");
                return Page();
            }

            Category.UserId = user.Id;
            Category.CreatedAt = DateTime.Now;
            Category.UpdatedAt = DateTime.Now;

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}
