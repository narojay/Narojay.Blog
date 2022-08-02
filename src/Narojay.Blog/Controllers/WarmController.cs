using Consul;
using Microsoft.AspNetCore.Mvc;

namespace Narojay.Blog.Controllers;

[Route("[controller]")]
public class WarmController : BaseController
{
    private readonly IConsulClient _client;

    public WarmController(IConsulClient client)
    {
        _client = client;
    }
    [HttpGet]
    public string Get()
    {
        return "OK";
    }
    
    [HttpPost]
    public string Post()
    {
        var consulClient = _client;
        return "1";
    }
}