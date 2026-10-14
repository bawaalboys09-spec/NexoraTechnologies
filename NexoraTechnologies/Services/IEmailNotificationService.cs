using NexoraTechnologies.Models;

namespace NexoraTechnologies.Services;

public interface IEmailNotificationService
{
    Task NotifyNewEnquiryAsync(ContactEnquiry enquiry, CancellationToken cancellationToken = default);
}
