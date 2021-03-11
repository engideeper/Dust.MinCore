using Dust.MinCore.Common;
using Dust.MinCore.Common.ApiClient;
using Dust.MinCore.Extensions;
using Dust.MinCore.Extensions.JsonWebToken;
using Dust.MinCore.Extensions.JsonWebToken.Model;
using Dust.MinCore.Models.Dtos.Input;
using Dust.MinCore.Models.Validators;
using Dust.MinCore.Server;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dust.MinCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <returns> </returns>
        [Authorize(policy: "Client")]
        [HttpGet("WriteLog")]
        public ApiResult GetWriteLog()
        {
            LogHelper.Default.Trace("测试");
            LogHelper.Default.Debug("测试");
            LogHelper.Default.Info("测试");
            LogHelper.Default.Warn("测试");
            LogHelper.Default.Error("测试");
            LogHelper.Default.Fatal("测试");
            return new ApiResult();
        }

        /// <summary>
        /// http请求
        /// </summary>
        /// <returns> </returns>
        [HttpGet("WebApiClient")]
        public async Task<ApiResult> GetWebApiClientAsync(
            [FromServices] IWeChatApi weChatApi,
            [FromServices] IConfiguration configuration, string code)
        {
            return new ApiResult(await weChatApi.GetOpenIdAsync(
                configuration["MiniProgram:Appid"],
                configuration["MiniProgram:Secret"],
                code));
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns> </returns>
        [HttpGet("GetToken")]
        public ApiResult GetToken(
            [FromServices] IWeChatApi weChatApi,
            [FromServices] IConfiguration configuration)
        {
            JwtSetting jwtSetting = configuration.GetSection("JwtSetting").Get<JwtSetting>();
            DateTime expiresAt = DateTime.Now.AddDays(jwtSetting.ExpireDays)
                                    .AddHours(jwtSetting.ExpireHours)
                                    .AddMinutes(jwtSetting.ExpireMinutes)
                                    .AddSeconds(jwtSetting.ExpireSeconds);

            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, "1"),
                    new Claim(JwtRegisteredClaimNames.Sid,"2"),
                    new Claim(ClaimTypes.Expiration, expiresAt.ToString()),
                    new Claim(ClaimTypes.Role, "Client"),
                    new Claim("AvatarUrl","3")
            };
            var token = JwtToken.BuildJwtToken(claims.ToArray(), jwtSetting);
            return new ApiResult(token);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns> </returns>
        [HttpPost("Login")]
        public async Task<ApiResult> Login(
            [FromServices] IWeChatApi weChatApi,
            [FromServices] IUserInfoServer userInfoServer,
            LoginInput loginInput)
        {
            ValidationResult results = new LoginInputValidator().Validate(loginInput);
            if (results.IsValid)
            {
                return new ApiResult(await userInfoServer.Login(loginInput));
            }
            else
            {
                return new ApiResult(results.Errors);
            }
        }
    }
}
