using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;


namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    async Task<bool> IRequestHandler<DeleteRestaurantCommand, bool>.Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant with {ResaurantId}", request.Id);
        bool result = await restaurantsRepository.DeleteRestaurantFromDBAsynce(request.Id);
        if (result)
        {
            logger.LogInformation("Restaurant with {ResaurantId} deleted successfully", request.Id);
            return true;
        }
        else
        {
            logger.LogWarning("Restaurant with {ResaurantId} not found", request.Id);

            return false;

        }

    }
}
