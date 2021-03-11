using Dust.MinCore.Common.ApiClient;
using Dust.MinCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Reflection;
using WebApiClient;

namespace Dust.MinCore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            #region 扩展服务

            services.AddAuthorizationSetup(Configuration);

            services.AddFreeSqlSetup(Configuration);

            services.AddSwaggerSetup();

            #endregion 扩展服务

            #region 服务注入 先后顺序注入  Dust.MinCore.Repository Dust.MinCore.Service

            services.AddScopedSetup("Dust.MinCore.Repository");
            services.AddScopedSetup("Dust.MinCore.Service");

            #endregion 服务注入 先后顺序注入  Dust.MinCore.Repository Dust.MinCore.Service

            #region 使用AutoMapper

            // 自动加载Service的程序集并从这里面找到继承了profile类的配置实现
            services.AddAutoMapper(Assembly.Load("Dust.MinCore.Models"));

            #endregion 使用AutoMapper

            #region 注入HttpApi

            services.AddHttpApi<IWeChatApi>();
            services.ConfigureHttpApi<IWeChatApi>(o =>
            {
                o.HttpHost = new Uri("https://api.weixin.qq.com/");
            });

            #endregion 注入HttpApi

            services.AddControllers(o =>
            {
                // 全局异常过滤
                o.Filters.Add(typeof(GlobalExceptionFilter));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwaggerMiddle();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
