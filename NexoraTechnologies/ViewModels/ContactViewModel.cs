using System.ComponentModel.DataAnnotations;

namespace NexoraTechnologies.ViewModels;

public class ContactViewModel
{
    [Required, StringLength(100, MinimumLength = 2)]
    [Display(Name = "Full name")]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [Phone, StringLength(30)]
    public string? Phone { get; set; }

    [StringLength(150)]
    public string? Company { get; set; }

    [Required]
    [Display(Name = "Service you need")]
    public string Service { get; set; } = string.Empty;

    [Display(Name = "Estimated budget")]
    public string? BudgetRange { get; set; }

    [Display(Name = "Preferred timeline")]
    public string? Timeline { get; set; }

    [Required, StringLength(5000, MinimumLength = 20)]
    public string Message { get; set; } = string.Empty;

    [Range(typeof(bool), "true", "true", ErrorMessage = "Please accept the privacy notice to submit this form.")]
    [Display(Name = "I agree to be contacted about this enquiry.")]
    public bool Consent { get; set; }
}
