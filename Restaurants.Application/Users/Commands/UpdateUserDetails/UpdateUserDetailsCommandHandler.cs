using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
namespace Restaurants.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger,IUserContext userContext, IUserStore<ApplicationUser> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("Updating user: {UserId}, with {@Request}", currentUser!.Id, request);

        var UserDb = await userStore.FindByIdAsync(currentUser.Id,cancellationToken);
        if (UserDb == null)
        {
            logger.LogWarning("User with ID {UserId} not found.", currentUser.Id);
            throw new NotFoundException(nameof(ApplicationUser), UserDb!.Id);
        }
        UserDb.BirthDate = request.BirthDate;
        UserDb.Nationality = request.Nationality;
        await userStore.UpdateAsync(UserDb, cancellationToken);
    }
}
