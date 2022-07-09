namespace Narojay.Blog.Infrastruct.Jwt;

public interface IJwtService
{
    string CreateJwtToken(string username);
}