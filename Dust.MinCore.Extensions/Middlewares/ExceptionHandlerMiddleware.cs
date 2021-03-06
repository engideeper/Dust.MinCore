using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Dust.MinCore.Extensions.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"> </param>
        /// <returns> </returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExceptionHandlerAsync(context, ex.Message);
            }
            finally
            {
                //var statusCode = context.Response.StatusCode;
                //var statusList=new List<int>() { 400, 401, 200, 403 };
                //if (!statusList.Contains(statusCode))
                //{
                //    Enum.TryParse(typeof(HttpStatusCode), statusCode.ToString(), out object message);
                //    await ExceptionHandlerAsync(context, message.ToString());
                //}
            }
        }

        /// <summary>
        /// 异常处理，返回JSON
        /// </summary>
        /// <param name="context"> </param>
        /// <param name="message"> </param>
        /// <returns> </returns>
        private async Task ExceptionHandlerAsync(HttpContext context, string message)
        {
            // context.Response.ContentType = "application/json;charset=utf-8";

            var result = new ApiResult(msg: message, statusCode: context.Response.StatusCode);
            var setting = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(result, Formatting.None, setting));
        }
    }
}
