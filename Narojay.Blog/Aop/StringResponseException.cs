using System;

namespace Narojay.Blog.Aop
{
    public class StringResponseException : ApplicationException
    {
        public StringResponseException(string message) : base(message)
        {
        }
    }
}