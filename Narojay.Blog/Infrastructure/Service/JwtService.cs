using Microsoft.IdentityModel.Tokens;
using Narojay.Blog.Configs;
using Narojay.Blog.Infrastructure.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Narojay.Blog.Infrastructure.Service
{
    public class JwtService : IJwtService
    {
        public string CreateJwtToken(string username)
        {
            //签名密钥
            string jwtKey = AppConfig.JwtSecret;
            //签发者
            string jwtIssuser = AppConfig.JwtValid;
            //接收者
            string jwtAudience = AppConfig.JwtValid;
            //令牌所承载的信息
            var claims = new[]
            {
                //用户Id
                new Claim("Name",username),
            };
            //获取对称密钥
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey));
            //使用has256加密密钥
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //生成token
            var token = new JwtSecurityToken(
                //签发者
                jwtIssuser,
                //接收者
                jwtAudience,
                //jwt令牌数据体
                claims,
                //令牌过期时间
                expires: DateTime.Now.AddHours(2),
                //为数字签名定义SecurityKey
                signingCredentials: creds
            );
            return "Bearer " + new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
