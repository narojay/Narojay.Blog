using Narojay.Blog.Infrastruct.Common;

namespace Narojay.Blog.Infrastruct.Jwt.Extension;

public static class JwtExtension
{
    public static void SetJwtConfig(string issuer, string audience, string secretKey)
    {
        JwtConfig.Issuer = issuer;
        JwtConfig.Audience = audience;
        JwtConfig.SecretKey = secretKey;
    }
}