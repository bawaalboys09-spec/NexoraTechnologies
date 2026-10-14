using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexoraTechnologies.Data;
using NexoraTechnologies.Models;

namespace NexoraTechnologies.Controllers;

[Authorize(Roles = "Client")]
[Route("Client")]
public class ClientController : Controller
{
    private readonly ApplicationDbContext _db;
    public ClientController(ApplicationDbContext db) => _db = db;

    [HttpGet("")]
    [HttpGet("Dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        var clientId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(clientId)) return Forbid();

        var projects = await _db.ClientProjects
            .Where(x => x.ClientId == clientId)
            .Include(x => x.Updates)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync();

        var tickets = await _db.SupportTickets
            .Where(x => x.ClientId == clientId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync();

        ViewBag.OpenTickets = tickets.Count(x => x.Status == "Open");
        ViewBag.Tickets = tickets;
        return View(projects);
    }

    [HttpPost("CreateTicket")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTicket(SupportTicket model)
    {
        var clientId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(clientId)) return Forbid();

        ModelState.Remove(nameof(model.ClientId));
        ModelState.Remove(nameof(model.Client));
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Please provide a subject and a message of at least 10 characters.";
            return RedirectToAction(nameof(Dashboard));
        }

        model.ClientId = clientId;
        model.Status = "Open";
        model.CreatedAtUtc = DateTime.UtcNow;
        _db.SupportTickets.Add(model);
        await _db.SaveChangesAsync();

        TempData["SuccessMessage"] = "Support ticket submitted.";
        return RedirectToAction(nameof(Dashboard));
    }
}
