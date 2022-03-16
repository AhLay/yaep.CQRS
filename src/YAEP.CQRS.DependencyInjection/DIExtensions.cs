using System.Reflection;
using YAEP.CQRS;
using YAEP.CQRS.Abstractions.Commands;
using YAEP.CQRS.Abstractions.Queries;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DIExtensions
    {
       
        public static IServiceCollection RegisterCommands(this IServiceCollection services, Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var types = assembly.GetTypes()
                        .Where(t => !t.IsInterface)
                        .Select(t => new
                        {
                            Implementation = t,
                            Abstraction = t.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                        }).Where(t => t.Abstraction.IsNotNull());

            foreach (var type in types)
            {
                services.Add(new ServiceDescriptor(type.Abstraction, type.Implementation, lifetime));
            }

            services.Add(new ServiceDescriptor(typeof(ICommandExecuter), typeof(CommandExecuter), lifetime));

            return services;
        }

        public static IServiceCollection RegisterQueries(this IServiceCollection services, Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var types = assembly.GetTypes()
                        .Where(t => !t.IsInterface)
                        .Select(t => new
                        {
                            Implementation = t,
                            Abstraction = t.GetInterfaces()
                            .FirstOrDefault(i => i.IsGenericType
                                              && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))
                        }).Where(t => t.Abstraction.IsNotNull()).ToArray();

            foreach (var type in types)
            {
                services.Add(new ServiceDescriptor(type.Abstraction, type.Implementation, lifetime));
            }

            services.Add(new ServiceDescriptor(typeof(IQueryExecuter), typeof(QueryExecuter), lifetime));

            return services;
        }

        public static IServiceCollection RegisterCommands(this IServiceCollection services, Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                services.RegisterCommands(assembly);
            }

            return services;
        }

    }
}
