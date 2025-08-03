
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seaders;
using Serilog;
namespace Restaurants.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

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
            var app = builder.Build();

            #region Seeding Data
            // use for seading the database if it is empty 
            //why we create a scope here? because we need to create a scope to use the dbcontext 
            var seader = app.Services.CreateScope().ServiceProvider.GetRequiredService<IRestaurantSeader>();
            seader.Seed();
            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
               
            }
            //midelware for Serilog 
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
