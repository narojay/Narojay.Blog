using System;

namespace Narojay.Blog.Application.Interface;

public class TestAService :ITestAService
{
    public void TestA()
    {
        Console.WriteLine(10);
    }
}