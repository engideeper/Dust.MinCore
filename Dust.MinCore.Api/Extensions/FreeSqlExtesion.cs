using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace Dust.MinCore.Api
{
    public static class FreeSqlExtesion
    {
        public static void AddFreeSqlSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            IFreeSql fsql = new FreeSqlBuilder().UseConnectionString(DataType.MySql, configuration.GetConnectionString("MySqlConnectionStr"))
                                                .UseAutoSyncStructure(true)
                                                .UseLazyLoading(true)
                                                .UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
                                                .Build();
            services.AddSingleton<IFreeSql>(fsql);
        }
    }
}
