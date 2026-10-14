using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexoraTechnologies.Data;
using NexoraTechnologies.Models;

namespace NexoraTechnologies.Services;

public static class DbInitializer
{
    public static async Task InitializeAsync(
        IServiceProvider services,
        string? adminEmail,
        string? adminPassword)
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureCreatedAsync();

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var role in new[] { "Admin", "Staff", "Client" })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded)
                    throw new InvalidOperationException("Could not create required roles: " +
                        string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }

        // Only create an admin when both environment variables are explicitly configured.
        if (!string.IsNullOrWhiteSpace(adminEmail) && !string.IsNullOrWhiteSpace(adminPassword))
        {
            var existing = await userManager.FindByEmailAsync(adminEmail);
            if (existing is null)
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "Nexora Administrator"
                };

                var created = await userManager.CreateAsync(admin, adminPassword);
                if (!created.Succeeded)
                    throw new InvalidOperationException("Admin creation failed: " +
                        string.Join("; ", created.Errors.Select(e => e.Description)));

                await userManager.AddToRoleAsync(admin, "Admin");
            }
            else if (!await userManager.IsInRoleAsync(existing, "Admin"))
            {
                await userManager.AddToRoleAsync(existing, "Admin");
            }
        }
    }
}
