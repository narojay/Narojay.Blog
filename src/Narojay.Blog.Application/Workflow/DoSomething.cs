using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Narojay.Blog.Application.Workflow;

public class DoSomething : StepBody
{
    private readonly IMyService _myService;

    public DoSomething(IMyService myService)
    {
        _myService = myService;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        _myService.DoTheThings();
        return ExecutionResult.Next();
    }
}