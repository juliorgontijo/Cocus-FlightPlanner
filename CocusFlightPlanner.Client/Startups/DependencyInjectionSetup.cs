using CocusFlightPlanner.Application.Infra;
using CocusFlightPlanner.Application.Repositories;
using CocusFlightPlanner.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace CocusFlightPlanner.Application.Startups
{
    public static class DependencyInjectionSetup
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IAircraftTypeService, AircraftTypeService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IRouteService, RouteService>();
        }
    }
}
