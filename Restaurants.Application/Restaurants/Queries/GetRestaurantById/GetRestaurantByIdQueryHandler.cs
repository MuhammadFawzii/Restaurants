using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    internal class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
    {
        public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Fetch restaurant that Id ={request.Id}");
            var restaurant = await restaurantsRepository.GetRestaurantByIdFromDBAsync(request.Id);
            if (restaurant is null)
            {
                logger.LogWarning($"Restaurant with Id {request.Id} not found.");
                return null;
            }
            logger.LogInformation($"Restaurant with Id {request.Id} found.");
            //this is from manuall mapping
            //var restaurantDto=RestaurantDto.FromEntity(restaurant);
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
    }
}
