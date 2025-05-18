using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ZimFund.Models;

namespace ZimFund.Middleware
{
    public class UserStatusMiddleware
    {
        private readonly RequestDelegate _next;

        public UserStatusMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var user = await userManager.GetUserAsync(context.User);

                if (user != null && user.IsDeleted)
                {
                    await signInManager.SignOutAsync();
                    context.Response.Redirect("/Identity/Account/Login?deactivated=true");
                    return;
                }
            }

            await _next(context);
        }
    }
}
