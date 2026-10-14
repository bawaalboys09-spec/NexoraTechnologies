using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexoraTechnologies.Data;

namespace NexoraTechnologies.Controllers;

[Authorize(Roles = "Client")]
public class ClientPortalController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var projects = await db.ClientProjects.AsNoTracking()
            .Where(x => x.ClientUserId == userId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync();
        return View(projects);
    }
}
