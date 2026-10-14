using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexoraTechnologies.Data;

namespace NexoraTechnologies.Controllers;

public class ServicesController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var items = await db.Services.AsNoTracking()
            .Where(x => x.IsPublished)
            .OrderBy(x => x.SortOrder)
            .ToListAsync();
        return View(items);
    }

    public async Task<IActionResult> Details(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug)) return NotFound();
        var item = await db.Services.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Slug == slug && x.IsPublished);
        return item is null ? NotFound() : View(item);
    }
}
