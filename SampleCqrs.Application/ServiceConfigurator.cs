using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SampleCqrs.Application.Bevaviors;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using static SmapleCqrs.Persistance.ServiceConfigurator;

namespace SampleCqrs.Application
{
    public static class ServiceConfigurator
    {
        public static void ConfigureApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.ConfigurePersistanceServices(configuration);
            //
            var applicationAssembly = typeof(SampleCqrs.Application.ServiceConfigurator).Assembly;
            // regiter Mediatr
            services.AddMediatR(applicationAssembly);
            // Wiring up FluentValidation Validator
            services.AddValidatorsFromAssembly(applicationAssembly);
            // Register Validation with fluent validation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));
            //
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryValidationBehavior<,>));
        }
    }
}
