using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Serilog;

namespace Restaurants.API.Extensions;
public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        //
        builder.Services.AddAuthentication();
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen(c =>
        {
            // Define a security scheme for Bearer token authentication.
            // This tells Swagger how to understand and expect a "Bearer" token in the authorization header.

            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http, // Specifies the type of security scheme as HTTP (e.g., Basic, Bearer).
                Scheme = "Bearer" // Defines the authentication scheme, in this case, "Bearer".
            });
            // Add a security requirement to all API operations that use the "bearerAuth" scheme.
            // This means that when you access an endpoint through Swagger UI, it will show an "Authorize" button
            // where you can input your Bearer token.

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    // Reference the security scheme defined above.
                    // This links the security requirement to the "bearerAuth" definition.
       
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
                        },
                        // An empty list of scopes. For JWT Bearer tokens, scopes are typically not used in this way
                        // as they are part of the token itself.
                        []
                    }

                });
        });
        // Adds services required for API endpoint discovery for Swagger/OpenAPI.
        // This is necessary for Swagger to automatically discover and document your API endpoints.
        builder.Services.AddEndpointsApiExplorer();
        //add services for the custom middleware for error handling 
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        //add services for the custom middleware for request time logging
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
        //add the Serilog coonfiguration 
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);



            //configuration
            //    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            //    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Information)
            //    .WriteTo.File("Logs/Log-Restaurant-API-", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
            //    .WriteTo.Console(outputTemplate: "[{Timestamp:dd-MM HH:mm:ss} {Level:u3}] | {SourceContext} | {NewLine}{Message:lj}{NewLine}{Exception}");
        });
    }
}
