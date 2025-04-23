using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;
using Microsoft.AspNetCore.Identity;

namespace ZimFund.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        [BindProperty]
        public Project Project { get; set; }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public SelectList CategoryOptions { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _context.Categories
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            CategoryOptions = new SelectList(categories, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync();
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            Project.UserId = user.Id;
            Project.CreatedAt = DateTime.UtcNow;
            Project.UpdatedAt = DateTime.UtcNow;
            Project.IsDeleted = false;

            if (ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                Project.Image = "/uploads/" + fileName;
            }

            _context.Projects.Add(Project);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private async Task LoadCategoriesAsync()
        {
            var categories = await _context.Categories
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            CategoryOptions = new SelectList(categories, "Id", "Name");
        }
    }
}
