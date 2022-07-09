using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;

namespace Narojay.Blog.Infrastruct.Email;

public class MailProvider : IMailProvider
{
    public MailProvider(EmailOption options)
    {
        Options = options;
    }

    public EmailOption Options { get; }
    public SmtpClient SmtpClient { get; }
    public Pop3Client Pop3Client { get; }
    public ImapClient ImapClient { get; }
}