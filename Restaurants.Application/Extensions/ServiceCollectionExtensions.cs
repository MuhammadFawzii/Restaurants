using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Domain.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;


namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddScoped<IRestaurantsService, RestaurantsService>();
        // this is services for automapper  
        services.AddAutoMapper(applicationAssembly);
        //this the servie for FluentValidation
        services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();
    }


}