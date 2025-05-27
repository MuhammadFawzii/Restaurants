using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seaders;
using Serilog;
using Restaurants.API.Extensions;
namespace Restaurants.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddPresentation();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

       
            var app = builder.Build();

            #region Seeding Data
            // use for seading the database if it is empty 
            //why we create a scope here? because we need to create a scope to use the dbcontext 
            var seader = app.Services.CreateScope().ServiceProvider.GetRequiredService<IRestaurantSeader>();
            seader.Seed();
            #endregion
            //midelware for error handling
            app.UseMiddleware<ErrorHandlingMiddleware>();
            //midelware for request time logging
            app.UseMiddleware<RequestTimeLoggingMiddleware>();
            //midelware for Serilog 
            app.UseSerilogRequestLogging();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();

            }


            app.UseHttpsRedirection();
            // this is middleware for authentication 
            app.MapGroup("api/identity")
                .WithTags("Identity")
                .MapIdentityApi<ApplicationUser>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
