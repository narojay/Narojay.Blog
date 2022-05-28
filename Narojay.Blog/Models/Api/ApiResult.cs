namespace Narojay.Blog.Models.Api
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }

        public string Code { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }
    }
}