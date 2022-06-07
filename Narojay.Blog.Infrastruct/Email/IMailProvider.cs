using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;

namespace Narojay.Blog.Infrastruct.Email
{
    public interface IMailProvider
    {

        EmailOption Options { get; }

        SmtpClient SmtpClient { get; }

        Pop3Client Pop3Client { get; }

        ImapClient ImapClient { get; }
    }
}
