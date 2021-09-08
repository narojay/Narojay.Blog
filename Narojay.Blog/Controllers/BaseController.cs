using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Narojay.Blog.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {

        public HttpContext HttpContext => ControllerContext.HttpContext;

        
    }
}
