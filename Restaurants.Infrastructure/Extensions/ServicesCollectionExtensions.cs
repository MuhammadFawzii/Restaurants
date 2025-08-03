
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seaders;
namespace Restaurants.Infrastructure.Extensions;

public static class ServicesCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RestaurantsDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                  .EnableSensitiveDataLogging());
        services.AddScoped<IRestaurantSeader, RestaurantSeader>();
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
    }


}
