using System.Reflection;

namespace First_App.Server
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(c =>
                c.RegisterServicesFromAssemblyContaining<Program>());

            return services;
        }
    }
}
