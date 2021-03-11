using Dust.MinCore.Extensions.JsonWebToken.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dust.MinCore.Extensions.JsonWebToken
{
    public class JwtToken
    {
        /// <summary>
        /// 获取基于JWT的Token
        /// </summary>
        /// <param name="claims"> 需要在登陆的时候配置 </param>
        /// <param name="jwtSetting"> 配置信息 </param>
        /// <returns> </returns>

        public static string BuildJwtToken(Claim[] claims, JwtSetting jwtSetting)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // 实例化JwtSecurityToken
            var jwtToken = new JwtSecurityToken(
                issuer: jwtSetting.Issuer,
                audience: jwtSetting.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(jwtSetting.ExpireDays)
                                     .AddHours(jwtSetting.ExpireHours)
                                     .AddMinutes(jwtSetting.ExpireMinutes)
                                     .AddSeconds(jwtSetting.ExpireSeconds),
                signingCredentials: creds
            );
            // 生成 Token
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
