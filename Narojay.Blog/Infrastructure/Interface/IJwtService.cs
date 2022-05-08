namespace Narojay.Blog.Infrastructure.Interface
{
    public interface IJwtService
    {
        string CreateJwtToken(string username);
    }
}