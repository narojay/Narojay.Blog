using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Narojay.Blog.Application.Service;

public class DemoHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        if (DateTime.Now.Second > 30) return Task.FromResult(HealthCheckResult.Healthy());

        return Task.FromResult(HealthCheckResult.Unhealthy("不健康"));
    }
}