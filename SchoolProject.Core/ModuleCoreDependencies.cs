using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Configuration of Fluent Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
