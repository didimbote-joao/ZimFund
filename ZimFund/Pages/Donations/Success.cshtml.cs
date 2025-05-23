using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using ZimFund.Models;
using ZimFund.Services; // Adicionado para usar o EmailService

namespace ZimFund.Pages.Donations
{
    public class SuccessModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailService _emailService;

        public SuccessModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, EmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<IActionResult> OnGetAsync(int projectId, decimal amount)
        {
            //var project = await _context.Projects.FindAsync(projectId);

            // Garante que o User (criador do projeto) seja carregado junto com o projeto
            var project = await _context.Projects
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return NotFound();
            }

            // Verificar se o usuário está logado
            var userId = User.Identity.IsAuthenticated
                ? _userManager.GetUserId(User)
                : "69d4ceb3-2779-47e7-ac85-658e143d03ee"; // ID anônimo

            var donation = new Donation
            {
                ProjectId = projectId,
                Amount = amount,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = userId,
                IsDeleted = false
            };

            _context.Donations.Add(donation);

            // Atualizar o valor arrecadado
            project.CollectedAmount += amount;
            project.UpdatedAt = DateTime.UtcNow;

            // Verificar se atingiu a meta e ainda não foi marcada como concluída
            if (!project.IsCompleted && project.CollectedAmount >= project.GoalAmount)
            {
                project.IsCompleted = true;

                var subject = "Parabéns! Sua campanha atingiu a meta!";
                var body = $@"
                    <div style='font-family: Arial, sans-serif; border: 1px solid #ddd; padding: 20px; border-radius: 10px; max-width: 600px; margin: auto;'>
                        <h2 style='color: #2b9348;'>Olá {project.User.FullName},</h2>
                        <p>Parabéns! Sua campanha <strong>{project.Title}</strong> atingiu o objetivo de arrecadação.</p>
                        <p><strong>Total arrecadado:</strong> {project.CollectedAmount:C}</p>
                        <p>Entraremos em contato para ajudá-lo com os próximos passos.</p>
                        <hr>
                        <p style='color: #666;'>Equipe <strong>ZimFund</strong></p>
                    </div>";
                // garantir que o e-mail só seja enviado quando o criador estiver realmente registrado
                if (project.User != null)
                {
                    await _emailService.SendEmailAsync(project.User.UserName, subject, body);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Donations/ThankYou", new { projectId = projectId, amount = amount });
        }
    }
}
