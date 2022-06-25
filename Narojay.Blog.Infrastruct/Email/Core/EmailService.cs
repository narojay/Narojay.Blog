namespace Narojay.Blog.Infrastruct.Email.Core;

public class EmailService : IEmailService
{
    public Task<bool> SendEmailAsync(EmailMessage emailMessage)
    {
        throw new NotImplementedException();
    }
}