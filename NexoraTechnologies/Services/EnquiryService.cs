using NexoraTechnologies.Data;
using NexoraTechnologies.Models;

namespace NexoraTechnologies.Services;

public class EnquiryService
{
    private readonly ApplicationDbContext _db;

    public EnquiryService(ApplicationDbContext db) => _db = db;

    public async Task CreateAsync(Enquiry enquiry, CancellationToken cancellationToken = default)
    {
        enquiry.Name = enquiry.Name.Trim();
        enquiry.Email = enquiry.Email.Trim().ToLowerInvariant();
        enquiry.Message = enquiry.Message.Trim();
        enquiry.CreatedAt = DateTime.UtcNow;
        enquiry.Status = "New";

        _db.Enquiries.Add(enquiry);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
