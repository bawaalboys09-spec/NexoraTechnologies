using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexoraTechnologies.Models;

namespace NexoraTechnologies.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider services, IConfiguration configuration)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // Starter convenience: creates the schema on first run. Before production, switch to EF Core migrations.
        await db.Database.EnsureCreatedAsync();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        foreach (var role in new[] { "Admin", "Client" })
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        if (!await db.Services.AnyAsync())
        {
            db.Services.AddRange(
                new ServiceItem { Title = "Website Development", Slug = "website-development", Summary = "Fast, responsive websites that make your business easy to find.", Description = "Business websites, landing pages, e-commerce experiences, and ongoing improvements.", IconName = "globe", SortOrder = 1 },
                new ServiceItem { Title = "Software Development", Slug = "software-development", Summary = "Custom software designed around your real business workflow.", Description = "Business applications, dashboards, integrations, and workflow tools built for maintainability.", IconName = "code", SortOrder = 2 },
                new ServiceItem { Title = "Mobile App Development", Slug = "mobile-app-development", Summary = "Mobile-first digital experiences for Android and iOS.", Description = "Plan, design, build, test, and maintain mobile applications for your users.", IconName = "smartphone", SortOrder = 3 },
                new ServiceItem { Title = "ERP Solutions", Slug = "erp-solutions", Summary = "Connect your people, processes, and operational data.", Description = "ERP and management systems tailored to schools, institutions, and businesses.", IconName = "layers", SortOrder = 4 },
                new ServiceItem { Title = "AI & Automation", Slug = "ai-automation", Summary = "Reduce repetitive work with practical AI and automation.", Description = "AI-enabled features, process automation, and system integrations with human oversight.", IconName = "sparkles", SortOrder = 5 },
                new ServiceItem { Title = "IT Support & Maintenance", Slug = "it-support-maintenance", Summary = "Keep your digital products healthy after launch.", Description = "Updates, monitoring, bug fixes, performance improvements, and technical support.", IconName = "shield-check", SortOrder = 6 }
            );
            await db.SaveChangesAsync();
        }

        var adminEmail = configuration["AdminSeed:Email"];
        var adminPassword = configuration["AdminSeed:Password"];
        if (!string.IsNullOrWhiteSpace(adminEmail) && !string.IsNullOrWhiteSpace(adminPassword))
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin is null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Nexora Administrator",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
                else
                    throw new InvalidOperationException("Admin seed failed: " + string.Join("; ", result.Errors.Select(e => e.Description)));
            }
            else if (!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
