using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleCqrs.Domain.Common;
using SampleCqrs.Domain.People;
using SmapleCqrs.Persistance.Repositories;

namespace SmapleCqrs.Persistance
{
    public static class ServiceConfigurator
    {
        public static void ConfigurePersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // registering database configuration to IOC Container
            services.AddEntityFrameworkNpgsql().AddDbContext<SampleCqrsDbContext>(optionsAction: opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("SampleCqrsConnection")
                    , builder =>
                    {
                        builder.MigrationsAssembly(typeof(ServiceConfigurator).Assembly.FullName);
                        //builder.UseNetTopologySuite();
                    });
            }, ServiceLifetime.Singleton);
            //  registering repository base to IOC Container
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // registering person repository to IOC Container
            services.AddScoped<IPersonRepository, PersonRepository>();
            //
        }
    }
}
