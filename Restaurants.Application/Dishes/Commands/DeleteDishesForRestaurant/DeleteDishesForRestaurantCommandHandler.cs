using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesForRestaurant
{
    public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository,IDishesRepository dishesRepository) : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
   
        async Task IRequestHandler<DeleteDishesForRestaurantCommand>.Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting all dishes for restaurant with {ResaurantId}", request.RestaurantId);
            bool result= await dishesRepository.DeleteDishesForRestaurantByIdFromDBAsync(request.RestaurantId);
            if (result)
            {
                logger.LogInformation("All dishes for restaurant with {ResaurantId} deleted successfully", request.RestaurantId);
            }
            else
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

        }
    }
}
