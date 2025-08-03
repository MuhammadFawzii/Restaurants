using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, IMapper mapper,
    IRestaurantsRepository  restaurantsRepository
    , IDishesRepository dishesRepository) : IRequestHandler<CreateDishCommand,int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding a new @{Dish} to the database.", request);
        var Dish = mapper.Map<Dish>(request);
        if (Dish == null)
        {
            logger.LogError("Failed to map CreateDishDto to Dish entity.");
            throw new InvalidOperationException("Mapping failed");
        }
        Restaurant? restaurant=await restaurantsRepository.GetRestaurantByIdFromDBAsync(request.RestaurantId);
        if(restaurant == null)
        {
            logger.LogError("Failed to find restaurant with Id {Id} in the database.", request.RestaurantId);
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }
        return await dishesRepository.AddDishesToDBAsynce(Dish);
    }
}
