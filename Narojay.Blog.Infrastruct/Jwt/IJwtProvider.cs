namespace Narojay.Blog.Infrastruct.Jwt;

public interface IJwtProvider
{
    JwtOptions JwtOptions { get; }
}

internal class JwtProvider : IJwtProvider
{
    public JwtProvider(JwtOptions jwtOptions)
    {
        JwtOptions = jwtOptions;
    }

    public JwtOptions JwtOptions { get; }
}