using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;
using Microsoft.AspNetCore.Authorization;

namespace ZimFund.Pages.Projects
{
    [Authorize(Roles = "admin,organizer")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public SelectList CategoryOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _context.Projects
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);

            if (Project == null)
            {
                return NotFound();
            }

            await LoadCategoriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync();
                return Page();
            }

            var project = await _context.Projects.FindAsync(Project.Id);
            if (project == null)
            {
                return NotFound();
            }

            // Conversão segura de GoalAmount (suporta vírgula ou ponto)
            var goalAmountStr = Request.Form["Project.GoalAmount"].ToString();
            if (!decimal.TryParse(goalAmountStr.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var parsedAmount))
            {
                ModelState.AddModelError("Project.GoalAmount", "Valor inválido para a meta de arrecadação.");
                await LoadCategoriesAsync();
                return Page();
            }

            // Atualiza campos
            project.Title = Project.Title;
            project.Description = Project.Description;
            project.GoalAmount = parsedAmount;
            project.CategoryId = Project.CategoryId;
            project.UpdatedAt = DateTime.UtcNow;

            // Se uma nova imagem foi enviada
            if (ImageFile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();
                const long maxFileSize = 2 * 1024 * 1024; // 2MB

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

                // Salva a nova imagem
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                project.Image = "/images/" + uniqueFileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }


        private async Task LoadCategoriesAsync()
        {
            var categories = await _context.Categories
                .Where(c => c.IsDeleted == false)
                .ToListAsync();

            CategoryOptions = new SelectList(categories, "Id", "Name");
        }
    }
}
