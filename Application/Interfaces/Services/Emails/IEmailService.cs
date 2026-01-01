

namespace Application.Interfaces.Services.Emails;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}
