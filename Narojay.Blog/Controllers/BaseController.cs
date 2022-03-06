using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Aop;

namespace Narojay.Blog.Controllers
{
    [ApiController, FormatResponse, ApiExceptionFilter]
    public class BaseController : ControllerBase
    {
    }
}
