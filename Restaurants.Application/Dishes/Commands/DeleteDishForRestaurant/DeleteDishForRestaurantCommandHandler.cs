

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishForRestaurant;

public class DeleteDishForRestaurantCommandHandler(ILogger<DeleteDishForRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository) : IRequestHandler<DeleteDishForRestaurantCommand>
{
    public async Task Handle(DeleteDishForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting  dish that dish id :{dishId} for restaurant with {ResaurantId}",request.DishId, request.RestaurantId);
        bool result = await dishesRepository.DeleteDishByIdForRestaurantByIdFromDBAsync(request.RestaurantId,request.DishId);
        if (result)
        {
            logger.LogInformation("Dish with {DishId} for restaurant with {ResaurantId} deleted successfully", request.DishId, request.RestaurantId);
        }
        else
        {
            throw new NotFoundException(nameof(Restaurant)+"Or "+nameof(Dish), request.RestaurantId.ToString() +" & "+ request.DishId.ToString() );

        }
    }
}
