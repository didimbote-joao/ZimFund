using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZimFund.Models;

namespace ZimFund.Pages.Users
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EditModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public EditUserViewModel Input { get; set; }

        public class EditUserViewModel
        {
            public string Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string Role { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            Input = new EditUserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Address = user.Address,
                Role = roles.FirstOrDefault() ?? "client"
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByIdAsync(Input.Id);
            if (user == null) return NotFound();

            // Verifica se o novo e-mail já está sendo usado por outro usuário
            var existingUser = await _userManager.FindByEmailAsync(Input.Email);
            if (existingUser != null && existingUser.Id != user.Id)
            {
                ModelState.AddModelError("Input.Email", "Este e-mail já está em uso por outro usuário.");
                return Page(); // Interrompe aqui se houver conflito
            }

            // Atualiza os dados do usuário
            user.FullName = Input.FullName;
            user.Email = Input.Email;
            user.UserName = Input.Email;
            user.Address = Input.Address;
            user.UpdatedAt = DateTime.UtcNow;

            // Atualiza o papel
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, Input.Role);

            // Salva alterações
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Erro ao atualizar o usuário.");
                return Page();
            }

            return RedirectToPage("Index");
        }

    }
}
