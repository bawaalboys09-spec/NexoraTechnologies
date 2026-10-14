using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexoraTechnologies.Models;

namespace NexoraTechnologies.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ServiceItem> Services => Set<ServiceItem>();
    public DbSet<ContactEnquiry> ContactEnquiries => Set<ContactEnquiry>();
    public DbSet<ClientProject> ClientProjects => Set<ClientProject>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ServiceItem>()
            .HasIndex(x => x.Slug)
            .IsUnique();

        builder.Entity<ContactEnquiry>()
            .HasIndex(x => new { x.Status, x.CreatedAtUtc });

        builder.Entity<ClientProject>()
            .HasIndex(x => x.ClientUserId);

        builder.Entity<ClientProject>()
            .HasCheckConstraint("CK_ClientProjects_ProgressPercent", "[ProgressPercent] >= 0 AND [ProgressPercent] <= 100");
    }
}
