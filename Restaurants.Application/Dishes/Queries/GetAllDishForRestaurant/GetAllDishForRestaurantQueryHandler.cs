using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;


namespace Restaurants.Application.Dishes.Queries.GetAllDishForRestaurant;

public class GetAllDishForRestaurantQueryHandler(ILogger<GetAllDishForRestaurantQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository) : IRequestHandler<GetAllDishForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetAllDishForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching all Dishes for Resturant from the database.");
        var restaurant= await restaurantsRepository.GetRestaurantByIdFromDBAsync(request.RestaurantId);
        if (restaurant==null)
        {
            throw new NotFoundException(nameof(restaurant),request.RestaurantId.ToString());
        }
        var dishes = await dishesRepository.GetAllDishesForRestaurantFromDBAsync(request.RestaurantId);
        return mapper.Map<List<DishDto>>(dishes);
    }
}
