using System.ComponentModel.DataAnnotations;

namespace NexoraTechnologies.Models;

public class SupportTicket
{
    public int Id { get; set; }

    [Required]
    public string ClientId { get; set; } = string.Empty;

    public ApplicationUser? Client { get; set; }

    [Required, StringLength(180)]
    public string Subject { get; set; } = string.Empty;

    [Required, StringLength(4000, MinimumLength = 10)]
    public string Description { get; set; } = string.Empty;

    [StringLength(30)]
    public string Status { get; set; } = "Open";

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
