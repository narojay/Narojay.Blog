using System.Threading.Tasks;

namespace Narojay.Blog.Infrastruct.Email.Core;

public interface IEmailService
{
    public Task<bool> SendEmailAsync(EmailMessage emailMessage);
}