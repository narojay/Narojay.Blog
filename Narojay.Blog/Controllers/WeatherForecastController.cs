using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Infrastructure;
using Narojay.Blog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DataContext _context;

        public ITestService TestService { get; set; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<List<Comment>> GetBlog()
        {
            var result = await _context.Comments.Where(x => x.UserCommentId == 5).ToListAsync();

            result.ForEach(x => x.BlogUser.Age = 2);

            await _context.SaveChangesAsync();

            return null;
        }
    }
}
