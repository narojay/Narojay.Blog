using System;
using Narojay.Blog.Application.Interface;
using Xunit;
using Xunit.Abstractions;

namespace Narojay.Blog.Test;

public class UnitTest1
{
    private readonly ITestAService _testAService;
    private readonly ITestService _testService;
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestAService testAService,ITestService testService, ITestOutputHelper testOutputHelper)
    {
        _testAService = testAService;
        _testService = testService;
        _testOutputHelper = testOutputHelper;
    }
    [Fact]
    public void Test1()
    {
        _testService.Test();
        Assert.Equal(1,1);
        _testOutputHelper.WriteLine("test");
    
    }
    
    [Fact]
    public void TestAService_TestA()
    {
        _testAService.TestA();
    
    }
}