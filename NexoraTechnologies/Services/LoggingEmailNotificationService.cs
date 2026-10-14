using NexoraTechnologies.Models;

namespace NexoraTechnologies.Services;

// Safe starter implementation: logs a notification event. Replace with a real transactional email provider before launch.
public class LoggingEmailNotificationService(ILogger<LoggingEmailNotificationService> logger) : IEmailNotificationService
{
    public Task NotifyNewEnquiryAsync(ContactEnquiry enquiry, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("New Nexora enquiry {EnquiryId} received for service {Service}. Email notification provider is not configured.",
            enquiry.Id, enquiry.Service);
        return Task.CompletedTask;
    }
}
