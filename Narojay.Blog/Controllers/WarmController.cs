using Microsoft.AspNetCore.Mvc;

namespace Narojay.Blog.Controllers
{
    [Route("[controller]")]
    public class WarmController : BaseController
    {
        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}