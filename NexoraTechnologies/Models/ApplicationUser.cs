using Microsoft.AspNetCore.Identity;

namespace NexoraTechnologies.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
