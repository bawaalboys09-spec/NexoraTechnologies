using System.ComponentModel.DataAnnotations;

namespace NexoraTechnologies.Models;

public class Enquiry
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [Phone, StringLength(30)]
    public string? Phone { get; set; }

    [StringLength(160)]
    public string? Company { get; set; }

    [Required, StringLength(100)]
    public string Service { get; set; } = string.Empty;

    [StringLength(40)]
    public string? BudgetRange { get; set; }

    [StringLength(40)]
    public string? Timeline { get; set; }

    [Required, StringLength(5000, MinimumLength = 10)]
    public string Message { get; set; } = string.Empty;

    [StringLength(30)]
    public string Status { get; set; } = "New";

    [Range(typeof(bool), "true", "true", ErrorMessage = "Please acknowledge the privacy notice.")]
    public bool PrivacyAcknowledged { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
