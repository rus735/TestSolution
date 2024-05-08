using DataManager.Models.Base;
using Microsoft.EntityFrameworkCore;
using TestSolution.Interfaces;
using TestSolution.Services;

namespace TestSolution.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                //options.UseLazyLoadingProxies();
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddScoped<Func<TodoContext>>((provider) => () => provider.GetService<TodoContext>());

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ITodoItems, TodoItemsServices>();
        }
    }
}