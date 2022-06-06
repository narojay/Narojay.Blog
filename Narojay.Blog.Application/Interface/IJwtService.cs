namespace Narojay.Blog.Application.Interface;

public interface IJwtService
{
    string CreateJwtToken(string username);
}