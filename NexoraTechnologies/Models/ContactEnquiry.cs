using System.ComponentModel.DataAnnotations;

namespace NexoraTechnologies.Models;

public class ContactEnquiry
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [StringLength(30)]
    public string? Phone { get; set; }

    [StringLength(150)]
    public string? Company { get; set; }

    [Required, StringLength(80)]
    public string Service { get; set; } = string.Empty;

    [StringLength(40)]
    public string? BudgetRange { get; set; }

    [StringLength(40)]
    public string? Timeline { get; set; }

    [Required, StringLength(5000, MinimumLength = 20)]
    public string Message { get; set; } = string.Empty;

    [Required]
    public string Status { get; set; } = "New";

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
