using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ZimFund.Pages.Projects
{
    [Authorize(Roles = "admin,organizer")] // contidiciona a pagina a apenas usuarios com nivel de acesso
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
                //Verifica aonde esta o erro
                foreach (var modelStateEntry in ModelState)
                {
                    foreach (var error in modelStateEntry.Value.Errors)
                    {
                        Console.WriteLine($"Erro no campo {modelStateEntry.Key}: {error.ErrorMessage}");
                    }
                }

                return Page();
            }

            Project.CreatedAt = DateTime.UtcNow;
            Project.UpdatedAt = DateTime.UtcNow;

            var user = await _userManager.GetUserAsync(User);
            Project.UserId = user.Id;

            // Validacao da imagem
            if (ImageFile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();
                const long maxFileSize = 2 * 1024 * 1024; // 2MB em bytes

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("ImageFile", "Apenas arquivos .jpg, .jpeg ou .png são permitidos.");
                    await LoadCategoriesAsync();
                    return Page();
                }

                if (ImageFile.Length > maxFileSize)
                {
                    ModelState.AddModelError("ImageFile", "O tamanho máximo permitido para a imagem é 2MB.");
                    await LoadCategoriesAsync();
                    return Page();
                }

                var fileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                Project.Image = "/images/" + fileName;
            }


            // Cria novo projecto
            _context.Projects.Add(Project);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
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
