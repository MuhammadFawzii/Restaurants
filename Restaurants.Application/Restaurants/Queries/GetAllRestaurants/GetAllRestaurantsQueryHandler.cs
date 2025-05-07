

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,IMapper mapper,IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching all restaurants from the database.");
        var restaurants = await restaurantsRepository.GetAllRestaurantsFromDBAsync();
        if (restaurants is null || !restaurants.Any())
        {
            logger.LogWarning("No restaurants found in the database.");
            return Enumerable.Empty<RestaurantDto>();
        }
        logger.LogInformation($"Found {restaurants.Count()} restaurants in the database.");
        //this is from manual mapping
        //var restaurantDtos = restaurants.Select(RestaurantDto.FromEntity).ToList();
        var restaurantDtos = mapper.Map<List<RestaurantDto>>(restaurants);
        return restaurantDtos;
    }
}
