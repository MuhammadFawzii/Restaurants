using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,IMapper mapper,IRestaurantsRepository restaurantsRepository):IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        Restaurant? oldRestaurant=await restaurantsRepository.GetRestaurantByIdFromDBAsync(request.Id);
        if (oldRestaurant == null)
        {
            logger.LogWarning("Restaurant with {ResaurantId} not found", request.Id);
            return false;
        }
        mapper.Map(request, oldRestaurant);

        int effectedRows = await restaurantsRepository.SaveAsync();
        if (effectedRows>0)
        {
            logger.LogInformation("Restaurant with {ResaurantId} updated successfully", request.Id);
            return true;
        }
        else
        {
            logger.LogWarning("Failed to update restaurant with {ResaurantId}", request.Id);
            return false;

        }
       


    }
}

