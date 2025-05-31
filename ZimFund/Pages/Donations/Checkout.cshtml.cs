using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZimFund.Services;
using ZimFund.Data;
using ZimFund.Models;

namespace ZimFund.Pages.Donations
{
    public class CheckoutModel : PageModel
    {
        private readonly StripePaymentService _stripePaymentService;
        private readonly ApplicationDbContext _context;

        public CheckoutModel(StripePaymentService stripePaymentService, ApplicationDbContext context)
        {
            _stripePaymentService = stripePaymentService;
            _context = context;
        }

        [BindProperty]
        public decimal Amount { get; set; }

        [BindProperty]
        public int ProjectId { get; set; }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (Amount <= 0)
        //    {
        //        ModelState.AddModelError(string.Empty, "O valor da doação deve ser maior que zero.");
        //        return Page();
        //    }

        //    var project = await _context.Projects.FindAsync(ProjectId);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    var successUrl = Url.Page("/Donations/Success", null, new { projectId = ProjectId, amount = Amount }, Request.Scheme);
        //    var cancelUrl = Url.Page("/Projects/Select", null, new { id = ProjectId }, Request.Scheme);

        //    var sessionUrl = await _stripePaymentService.CreateCheckoutSessionAsync(Amount, project.Title, successUrl, cancelUrl);

        //    return Redirect(sessionUrl);
        //}

        public async Task<IActionResult> OnPostAsync()
        {
            if (Amount <= 0)
            {
                ModelState.AddModelError(string.Empty, "O valor da doação deve ser maior que zero.");
                return Page();
            }

            var project = await _context.Projects.FindAsync(ProjectId);
            if (project == null)
            {
                return NotFound();
            }

            var successUrl = Url.Page("/Donations/Success", null, new { projectId = ProjectId, amount = Amount }, Request.Scheme);
            var cancelUrl = Url.Page("/Projects/Select", null, new { id = ProjectId }, Request.Scheme);

            try
            {
                var sessionUrl = await _stripePaymentService.CreateCheckoutSessionAsync(Amount, project.Title, successUrl, cancelUrl);
                return Redirect(sessionUrl);
            }
            catch (ApplicationException ex)
            {
                // Redireciona de volta com a mensagem de erro
                TempData["StripeError"] = ex.Message;
                return RedirectToPage("/Projects/Select", new { id = ProjectId });
            }
        }

    }
}
