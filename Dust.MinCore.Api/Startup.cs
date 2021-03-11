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

            #region ��չ����

            services.AddAuthorizationSetup(Configuration);

            services.AddFreeSqlSetup(Configuration);

            services.AddSwaggerSetup();

            #endregion ��չ����

            #region ����ע�� �Ⱥ�˳��ע��  Dust.MinCore.Repository Dust.MinCore.Service

            services.AddScopedSetup("Dust.MinCore.Repository");
            services.AddScopedSetup("Dust.MinCore.Service");

            #endregion ����ע�� �Ⱥ�˳��ע��  Dust.MinCore.Repository Dust.MinCore.Service

            #region ʹ��AutoMapper

            // �Զ�����Service�ĳ��򼯲����������ҵ��̳���profile�������ʵ��
            services.AddAutoMapper(Assembly.Load("Dust.MinCore.Models"));

            #endregion ʹ��AutoMapper

            #region ע��HttpApi

            services.AddHttpApi<IWeChatApi>();
            services.ConfigureHttpApi<IWeChatApi>(o =>
            {
                o.HttpHost = new Uri("https://api.weixin.qq.com/");
            });

            #endregion ע��HttpApi

            services.AddControllers(o =>
            {
                // ȫ���쳣����
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
