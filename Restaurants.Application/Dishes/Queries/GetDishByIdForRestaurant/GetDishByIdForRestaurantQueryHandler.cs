using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetAllDishForRestaurant;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
public class GetDishByIdForRestaurantQueryHandler(ILogger<GetAllDishForRestaurantQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching  Dish {dishId} for Resturant that id = {restId} from the database.",request.DishId,request.RestaurantId);
        var restaurant = await restaurantsRepository.GetRestaurantByIdFromDBAsync(request.RestaurantId);
        if (restaurant==null)
        {
            throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());
        }
        var dish = await dishesRepository.GetDishByIdForRestaurantFromDBAsync(request.RestaurantId, request.DishId);
        if (dish==null)
        {
            throw new NotFoundException(nameof(dish), request.DishId.ToString());
        }
        return mapper.Map<DishDto>(dish);
    }
}
