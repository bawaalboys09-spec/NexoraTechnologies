using System.ComponentModel.DataAnnotations;

namespace NexoraTechnologies.Models;

public class ClientProject
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    [Required]
    public string ClientUserId { get; set; } = string.Empty;

    [StringLength(40)]
    public string Status { get; set; } = "Planning";

    public int ProgressPercent { get; set; }

    public DateTime? TargetDateUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
