using Microsoft.EntityFrameworkCore;
using ZimFund.Data;
using Microsoft.AspNetCore.Identity;
using ZimFund.Models;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using ZimFund.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Adicionar base de dados
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Adicionar Scaffold do Identity e UserRole
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Adicionar Stripe
Stripe.StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
builder.Services.AddTransient<StripePaymentService>();

// Adicionar Envio de email
builder.Services.AddTransient<EmailService>();

// Define cultura
var cultureInfo = new CultureInfo("pt-AO");
cultureInfo.NumberFormat.CurrencySymbol = "Kz";

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<ZimFund.Middleware.UserStatusMiddleware>();

app.UseAuthorization();

app.MapRazorPages();

app.Run();