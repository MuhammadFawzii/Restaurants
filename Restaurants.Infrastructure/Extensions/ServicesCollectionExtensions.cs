
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seaders;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Restaurants.Infrastructure.Authorization;
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
        services.AddScoped<IDishesRepository, DishesRepository>();
        //this service is used to add the identity services to the application
        //used to add built-in Identity endpoints (like login, register, logout, etc.)
        //This registers minimal API endpoints for Identity management into your application.
        //This method tells ASP.NET Core Identity to use Entity Framework Core for storing and managing user and role data in a SQL database (or other EF-supported database).
        services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();
    }


}
