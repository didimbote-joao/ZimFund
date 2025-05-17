using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZimFund.Models;

namespace ZimFund.Pages.Users
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public ApplicationUser UserToDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            UserToDelete = await _userManager.FindByIdAsync(id);
            if (UserToDelete == null || UserToDelete.IsDeleted)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(User);
            if (user.Id == currentUserId)
            {
                ModelState.AddModelError(string.Empty, "Você não pode excluir a si mesmo.");
                UserToDelete = user; // Necessário para manter a tela com os dados
                return Page();
            }

            user.IsDeleted = true;
            user.UpdatedAt = DateTime.Now;
            await _userManager.UpdateAsync(user);

            return RedirectToPage("Index");
        }
    }
}
