using System;

namespace Narojay.Blog.Domain;

public class StringResponseException : ApplicationException
{
    public StringResponseException(string message) : base(message)
    {
    }
}