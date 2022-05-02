using System;

namespace Narojay.Blog.Aop
{
    public class FriendlyException : ApplicationException
    {
        public FriendlyException(string message) : base(message)
        {
        }
    }
}