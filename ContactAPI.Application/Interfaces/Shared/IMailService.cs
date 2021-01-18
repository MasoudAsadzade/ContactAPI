using ContactAPI.Application.DTOs.Mail;
using System.Threading.Tasks;

namespace ContactAPI.Application.Interfaces.Shared
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}