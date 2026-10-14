using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexoraTechnologies.Data;
using NexoraTechnologies.Models;

namespace NexoraTechnologies.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(ApplicationDbContext db, UserManager<ApplicationUser> userManager) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.EnquiryCount = await db.ContactEnquiries.CountAsync();
        ViewBag.NewEnquiryCount = await db.ContactEnquiries.CountAsync(x => x.Status == "New");
        var clients = await userManager.GetUsersInRoleAsync("Client");
        ViewBag.ClientCount = clients.Count;
        ViewBag.ServiceCount = await db.Services.CountAsync();
        var latest = await db.ContactEnquiries.AsNoTracking()
            .OrderByDescending(x => x.CreatedAtUtc)
            .Take(8).ToListAsync();
        return View(latest);
    }

    public async Task<IActionResult> Enquiries(string? status)
    {
        var query = db.ContactEnquiries.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(x => x.Status == status);
        ViewBag.CurrentStatus = status;
        return View(await query.OrderByDescending(x => x.CreatedAtUtc).ToListAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateEnquiryStatus(int id, string status)
    {
        var allowed = new[] { "New", "Contacted", "Qualified", "Closed", "Spam" };
        if (!allowed.Contains(status)) return BadRequest("Invalid status.");

        var enquiry = await db.ContactEnquiries.FindAsync(id);
        if (enquiry is null) return NotFound();
        enquiry.Status = status;
        await db.SaveChangesAsync();
        TempData["SuccessMessage"] = "Enquiry status updated.";
        return RedirectToAction(nameof(Enquiries));
    }
}
