using System.Threading.Tasks;

namespace EACA_API.Services.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
