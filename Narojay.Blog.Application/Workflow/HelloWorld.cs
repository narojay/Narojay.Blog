using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Narojay.Blog.Application.Workflow;

public class HelloWorld : StepBody
{
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine("Hello world");
        return ExecutionResult.Next();
    }
}