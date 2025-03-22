using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZimFund.Models;

namespace ZimFund.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Donation> Donations { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Definindo roles
            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var client = new IdentityRole("client");
            client.NormalizedName = "client";

            var organizer = new IdentityRole("organizer");
            organizer.NormalizedName = "organizer";


            // Adicionando as roles à tabela AspNetRoles
            builder.Entity<IdentityRole>().HasData(admin, client, organizer);

            // Precision para valores monetários
            builder.Entity<Project>()
                .Property(p => p.GoalAmount)
                .HasPrecision(18, 2);
            builder.Entity<Project>()
                .Property(p => p.CollectedAmount)
                .HasPrecision(18, 2);
            builder.Entity<Donation>()
                .Property(d => d.Amount)
                .HasPrecision(18, 2);

            // Configurando relacionamentos para evitar cascata múltipla
            builder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Project>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Donation>()
                .HasOne(d => d.User)
                .WithMany(u => u.Donations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Donation>()
                .HasOne(d => d.Project)
                .WithMany(p => p.Donations)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
 }

