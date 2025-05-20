using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;


namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand>
{
    async Task IRequestHandler<DeleteRestaurantCommand>.Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant with {ResaurantId}", request.Id);
        bool result = await restaurantsRepository.DeleteRestaurantFromDBAsynce(request.Id);
        if (result)
        {
            logger.LogInformation("Restaurant with {ResaurantId} deleted successfully", request.Id);
        }
        else
        {
            throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
        }

    }
}