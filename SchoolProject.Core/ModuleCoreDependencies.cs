using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace SchoolProject.Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            // Configuration of Mediator
            services.AddMediatR(confg => confg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            // Configuration of Auto Mapper

            var configExpression = new MapperConfigurationExpression();

            configExpression.AddMaps(AppDomain.CurrentDomain.GetAssemblies());

            var loggerFactory = services.BuildServiceProvider()
                .GetRequiredService<ILoggerFactory>();

            var mapperConfig = new MapperConfiguration(configExpression, loggerFactory);

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton<IMapper>(mapper);
            // Configuration of Fluent Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }

    internal class ApplicationAssemblyMarker
    {
    }
}
