using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger,IMapper mapper) : IRestaurantsService
    {
        public async Task<int> AddRestaurantAsynce(CreateRestaurantDto restaurantDto)
        {

            logger.LogInformation("Adding a new restaurant to the database.");
            var restaurant =mapper.Map<Restaurant>(restaurantDto);
            if (restaurant == null)
            {
                logger.LogError("Failed to map CreateRestaurantDto to Restaurant entity.");
                throw new InvalidOperationException("Mapping failed");
            }
            int restaurantId = await restaurantsRepository.AddRestaurantToDBAsynce(restaurant);
            if (restaurantId == 0)
            {
                logger.LogError("Failed to add restaurant to the database.");
                throw new InvalidOperationException("Adding restaurant failed");
            }
            logger.LogInformation($"Restaurant with Id {restaurantId} added to the database.");
            return restaurantId;
        }

        public async Task<IEnumerable<RestaurantDto?>> GetAllRestaurantsAsync()
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
            var restaurantDtos =mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantDtos;
        }

        public async Task<RestaurantDto?> GetRestaurantByIdAsynce(int id)
        {
            logger.LogInformation($"Fetch restaurant that Id ={id}");
            var restaurant = await restaurantsRepository.GetRestaurantByIdFromDBAsync(id);
            if (restaurant is null)
            {
                logger.LogWarning($"Restaurant with Id {id} not found.");
                return null;
            }
            logger.LogInformation($"Restaurant with Id {id} found.");
            //this is from manuall mapping
            //var restaurantDto=RestaurantDto.FromEntity(restaurant);
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
    }
}
