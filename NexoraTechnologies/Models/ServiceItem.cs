using System.ComponentModel.DataAnnotations;

namespace NexoraTechnologies.Models;

public class ServiceItem
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(120)]
    public string Slug { get; set; } = string.Empty;

    [Required, StringLength(500)]
    public string Summary { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [StringLength(60)]
    public string IconName { get; set; } = "code";

    public int SortOrder { get; set; }
    public bool IsPublished { get; set; } = true;
}
