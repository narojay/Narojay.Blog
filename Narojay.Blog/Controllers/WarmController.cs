using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
