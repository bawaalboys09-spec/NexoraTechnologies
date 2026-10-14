using System.ComponentModel.DataAnnotations;

namespace NexoraTechnologies.Models;

public class ProjectUpdate
{
    public int Id { get; set; }

    public int ClientProjectId { get; set; }
    public ClientProject? Project { get; set; }

    [Required, StringLength(2000)]
    public string Message { get; set; } = string.Empty;

    [StringLength(120)]
    public string CreatedBy { get; set; } = "Nexora Team";

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
