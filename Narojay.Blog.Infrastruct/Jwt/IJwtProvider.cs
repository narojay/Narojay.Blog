namespace Narojay.Blog.Infrastruct.Jwt
{
    public interface IJwtProvider
    {

        JwtOptions JwtOptions { get; }
    }

    internal class JwtProvider : IJwtProvider
    {
        public JwtOptions JwtOptions { get; }

        public JwtProvider(JwtOptions jwtOptions)
        {
            JwtOptions = jwtOptions;
        }
    }
}
