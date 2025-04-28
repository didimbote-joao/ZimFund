using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZimFund.Data;
using ZimFund.Models;

namespace ZimFund.Pages.Donations
{
    public class SuccessModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuccessModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int projectId, decimal amount)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            // Verificar se o usuário está logado
            var userId = User.Identity.IsAuthenticated
                ? _userManager.GetUserId(User)
                : "deea36ba-1f53-497f-ad32-ecaada12ae51"; // ID anônimo

            var donation = new Donation
            {
                ProjectId = projectId,
                Amount = amount,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = userId,
                IsDeleted = false
            };

            // Adiciona a doacao
            _context.Donations.Add(donation);

            // Atualiza também o valor arrecadado no projeto
            project.CollectedAmount += amount;
            project.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Redericiona para a pagina de agradecimento
            return RedirectToPage("/Donations/ThankYou", new { projectId = projectId, amount = amount });

        }
    }
}
