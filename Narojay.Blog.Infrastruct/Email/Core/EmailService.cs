using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narojay.Blog.Infrastruct.Email.Core
{
    public class EmailService : IEmailService
    {
        public Task<bool> SendEmailAsync(EmailMessage emailMessage)
        {
            throw new NotImplementedException();
        }
    }
}
