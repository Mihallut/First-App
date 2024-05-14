using First_App.Server;
using First_App.Server.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace First_App.Test
{
    public class FirstAppFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ApiDbContext>));
                services.AddNpgsql<ApiDbContext>("Host=localhost;Port=5432;Database=TaskBoard_Test;Username=postgres;Password=04dNxnW2i2;Include Error Detail=True;");
                var context = CreateDbContext(services);
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            });
            base.ConfigureWebHost(builder);
        }

        private static ApiDbContext CreateDbContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<ApiDbContext>();
        }
    }
}
