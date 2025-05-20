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
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    internal class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            logger.LogInformation("Fetching restaurant with Id = {RestaurantId}", request.Id);

            var restaurant = await restaurantsRepository.GetRestaurantByIdFromDBAsync(request.Id);
            if (restaurant == null)
            {
               throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }

            logger.LogInformation("Restaurant with Id = {RestaurantId} found.", request.Id);

            // Map the restaurant entity to a DTO
            return mapper.Map<RestaurantDto>(restaurant);
        }
    }
}
