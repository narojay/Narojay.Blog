using Serilog;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Narojay.Blog.Application.Workflow;

public class TestWorkflow : IWorkflow<MyDataClass>
{
    public string Id => "TestWorkflow";

    public int Version => 1;

    public void Build(IWorkflowBuilder<MyDataClass> builder)
    {
        builder
            .StartWith(context => ExecutionResult.Next())
            .WaitFor("MyEvent", (data, context) => context.Workflow.Id, data => DateTime.Now)
            .Output(data => data.Value1, step => step.EventData)
            .Then(context => Log.Information("workflow complete"));
    }
}

public class MyDataClass
{
    public string Value1 { get; set; }
}