using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ZimFund.Models;

namespace ZimFund.Pages.Users
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public List<string> Roles { get; set; } = new() { "client", "organizer", "admin" };

        public class InputModel
        {
            [Required(ErrorMessage = "O nome completo � obrigat�rio.")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "O email � obrigat�rio.")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "O telefone � obrigat�rio.")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "O endere�o � obrigat�rio.")]
            public string Address { get; set; }

            [Required(ErrorMessage = "A senha � obrigat�ria.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "A confirma��o da senha � obrigat�ria.")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "As senhas n�o coincidem.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "O papel do usu�rio � obrigat�rio.")]
            public string Role { get; set; }
        }

        public void OnGet()
        {
            // Apenas para carregar a p�gina
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new ApplicationUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                FullName = Input.FullName,
                PhoneNumber = Input.PhoneNumber,
                Address = Input.Address,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                // Verifica se o role existe, sen�o cria
                if (!await _roleManager.RoleExistsAsync(Input.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(Input.Role));
                }

                await _userManager.AddToRoleAsync(user, Input.Role);

                TempData["SuccessMessage"] = "Usu�rio criado com sucesso!";
                return RedirectToPage("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
