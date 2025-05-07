using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    internal class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,IMapper mapper,IRestaurantsRepository restaurantsRepository) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {

            logger.LogInformation("Adding a new restaurant to the database.");
            var restaurant = mapper.Map<Restaurant>(request);
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
    }
}
