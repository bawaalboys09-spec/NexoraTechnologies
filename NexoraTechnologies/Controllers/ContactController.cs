using Microsoft.AspNetCore.Mvc;
using NexoraTechnologies.Data;
using NexoraTechnologies.Models;
using NexoraTechnologies.Services;
using NexoraTechnologies.ViewModels;

namespace NexoraTechnologies.Controllers;

public class ContactController(
    ApplicationDbContext db,
    IEmailNotificationService emailNotificationService,
    ILogger<ContactController> logger) : Controller
{
    [HttpGet]
    public IActionResult Index() => View(new ContactViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ContactViewModel model, CancellationToken cancellationToken)
    {
        var allowedServices = new[] { "Website Development", "Software Development", "Mobile App Development", "ERP Solutions", "AI & Automation", "IT Support & Maintenance", "Other" };
        if (!allowedServices.Contains(model.Service))
            ModelState.AddModelError(nameof(model.Service), "Please select a valid service.");

        var allowedBudgets = new[] { "", "Under ₹25,000", "₹25,000–₹75,000", "₹75,000–₹2,00,000", "₹2,00,000+", "Not sure" };
        if (model.BudgetRange is not null && !allowedBudgets.Contains(model.BudgetRange))
            ModelState.AddModelError(nameof(model.BudgetRange), "Please select a valid budget range.");

        var allowedTimelines = new[] { "", "ASAP", "2–4 weeks", "1–3 months", "Flexible" };
        if (model.Timeline is not null && !allowedTimelines.Contains(model.Timeline))
            ModelState.AddModelError(nameof(model.Timeline), "Please select a valid timeline.");

        if (!ModelState.IsValid) return View(model);

        var enquiry = new ContactEnquiry
        {
            Name = model.Name.Trim(),
            Email = model.Email.Trim().ToLowerInvariant(),
            Phone = string.IsNullOrWhiteSpace(model.Phone) ? null : model.Phone.Trim(),
            Company = string.IsNullOrWhiteSpace(model.Company) ? null : model.Company.Trim(),
            Service = model.Service,
            BudgetRange = model.BudgetRange,
            Timeline = model.Timeline,
            Message = model.Message.Trim(),
            Status = "New",
            CreatedAtUtc = DateTime.UtcNow
        };

        db.ContactEnquiries.Add(enquiry);
        await db.SaveChangesAsync(cancellationToken);

        try
        {
            await emailNotificationService.NotifyNewEnquiryAsync(enquiry, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to notify about enquiry {EnquiryId}", enquiry.Id);
        }

        TempData["SuccessMessage"] = "Thank you. Your enquiry has been received. Our team will review it and get back to you.";
        return RedirectToAction(nameof(ThankYou));
    }

    [HttpGet]
    public IActionResult ThankYou() => View();
}
