﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Narojay.Blog.Infrastruct.Common;

namespace Narojay.Blog.Infrastruct.Jwt;

public class JwtService : IJwtService
{
    public string CreateJwtToken(string username)
    {
        //签名密钥
        var jwtKey = JwtConfig.SecretKey;
        //签发者
        var jwtIssuser = JwtConfig.Issuer;
        //接收者
        var jwtAudience = JwtConfig.Audience;
        //令牌所承载的信息
        var claims = new[]
        {
            //用户Idjn 
            new Claim("Name", username)
        };
        //获取对称密钥
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
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