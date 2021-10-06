using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface IJwtService
    {
        string CreateJwtToken(string username);
    }
}
