using First_App.Server.Helpers;
using First_App.Server.Helpers.Interfases;
using First_App.Server.Repositories.Classes;
using First_App.Server.Repositories.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace First_App.Server
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskListRepository, TaskListRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
            services.AddScoped<IActivityLogGenerator, ActivityLogGenerator>();
            services.AddScoped<IBoardRepository, BoardRepository>();
            return services;
        }
    }
}
