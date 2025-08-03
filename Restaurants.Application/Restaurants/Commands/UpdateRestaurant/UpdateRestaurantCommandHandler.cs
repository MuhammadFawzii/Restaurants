using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,IMapper mapper,IRestaurantsRepository restaurantsRepository):IRequestHandler<UpdateRestaurantCommand>
{
    public async Task  Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        Restaurant? oldRestaurant=await restaurantsRepository.GetRestaurantByIdFromDBAsync(request.Id);
        if (oldRestaurant == null)
        {
            throw new NotFoundException (nameof(Restaurant), request.Id.ToString());
        }
        mapper.Map(request, oldRestaurant);

        int effectedRows = await restaurantsRepository.SaveAsync();
        if (effectedRows>0)
        {
            logger.LogInformation("Restaurant with {ResaurantId} updated successfully", request.Id);
        }
        else
        {
            logger.LogWarning("Failed to update restaurant with {ResaurantId}", request.Id);
        }
       


    }
}

