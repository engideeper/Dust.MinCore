using Dust.MinCore.Common.WeChat;
using WebApiClient;
using WebApiClient.Attributes;

namespace Dust.MinCore.Common.ApiClient
{
    public interface IWeChatApi : IHttpApi
    {
        /**
         *注入
        services.AddHttpApi<IWeChatApi>();
        services.ConfigureHttpApi<IWeChatApi>(o=>
        {
            o.HttpHost = new Uri("https://api.weixin.qq.com/");
        });
         */

        [JsonReturnAttribute]
        [HttpGet("sns/jscode2session")]
        ITask<Code2Session> GetOpenIdAsync(string appid, string secret, string js_code, string grant_type = "authorization_code");
    }
}
