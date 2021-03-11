using Dust.MinCore.Common.Attribute;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dust.MinCore.Extensions
{
    public static class ServiceInjectExtesion
    {
        public static void AddTransientSetup(this IServiceCollection services, string assemblyString)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var assembly = Assembly.Load(assemblyString);
            IList<Type> types = assembly.GetTypes().ToList();
            foreach (Type item in types)
            {
                Transient attribute = item.GetCustomAttribute(typeof(Transient), false) as Transient;
                if (attribute != null)
                {
                    Type type = types.Where(o => o.Name == attribute.IName).First();
                    services.AddTransient(type, item);
                }
            }
        }

        public static void AddScopedSetup(this IServiceCollection services, string assemblyString)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var assembly = Assembly.Load(assemblyString);
            IList<Type> types = assembly.GetTypes().ToList();
            foreach (Type item in types)
            {
                Scoped attribute = item.GetCustomAttribute(typeof(Scoped), false) as Scoped;
                if (attribute != null)
                {
                    Type type = types.Where(o => o.Name == attribute.IName).First();
                    services.AddScoped(type, item);
                }
            }
        }

        public static void AddSingletonSetup(this IServiceCollection services, string assemblyString)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var assembly = Assembly.Load(assemblyString);
            IList<Type> types = assembly.GetTypes().ToList();
            foreach (Type item in types)
            {
                Singleton attribute = item.GetCustomAttribute(typeof(Singleton), false) as Singleton;
                if (attribute != null)
                {
                    Type type = types.Where(o => o.Name == attribute.IName).First();
                    services.AddSingleton(type, item);
                }
            }
        }
    }
}
