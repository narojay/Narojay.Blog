using System;
using Narojay.Blog.Application.Interface;

namespace Narojay.Blog.Application.Service;

public class TestAService :ITestAService
{
    public void TestA()
    {
        Console.WriteLine(10);
    }
}