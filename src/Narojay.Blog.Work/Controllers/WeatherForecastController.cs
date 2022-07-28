using System;
using System.Collections.Generic;
using System.Linq;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Application.Events;

namespace Narojay.Blog.Work.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IWebHostEnvironment _environment;
    private readonly IIntegrationEventHandler<CreateOrderEvent> _handler;

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWebHostEnvironment environment,
        IIntegrationEventHandler<CreateOrderEvent> handler)
    {
        _logger = logger;
        _environment = environment;
        _handler = handler;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _handler.Handle(new CreateOrderEvent());
        _logger.LogInformation(_environment.EnvironmentName);
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}