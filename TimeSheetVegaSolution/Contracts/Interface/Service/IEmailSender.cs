using Model.Email;
using System.Threading.Tasks;

namespace Contracts.Interface.Service
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}
