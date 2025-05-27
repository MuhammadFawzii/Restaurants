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
using Restaurants.Application.Users;


namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        // this is services for automapper  
        services.AddAutoMapper(applicationAssembly);
        //this the servie for FluentValidation
        services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();

        //this the sevice for MediatR CORs
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        // Enables access to the current HTTP context throughout your application.
        services.AddHttpContextAccessor();
        // Registers your custom user context service, making user information available per request.
        services.AddScoped<IUserContext, UserContext>();
    }


}