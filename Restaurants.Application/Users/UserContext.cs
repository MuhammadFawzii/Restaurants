using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.Users;

public interface IUserContext
{
    public CurrentUser? GetCurrentUser();

}
internal class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    /// <summary>
    /// Retrieves the current authenticated user's details from the HTTP context.
    /// </summary>
    /// <returns>
    /// A <see cref="CurrentUser"/> object if the user is authenticated, otherwise <see langword="null"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the HTTP context is not available or not set up correctly.
    /// </exception>
    public CurrentUser? GetCurrentUser()
    {
        // Access the current HttpContext and its User (ClaimsPrincipal) from the IHttpContextAccessor.
        // The null-conditional operator '?' ensures that if HttpContext is null, currentUser also becomes null.
        var currentUser = httpContextAccessor.HttpContext?.User;

        // If currentUser is null, it means there's no active HTTP context or the HttpContext.User is unavailable.
        // This usually indicates a configuration issue or an attempt to access user context outside of an HTTP request.
        if (currentUser == null)
            throw new InvalidOperationException("User context is not available. Ensure that the HTTP context is set up correctly.");

        // Check if the user's identity is not available or if the user is not authenticated.
        // If either is true, it means there's no valid, authenticated user for the current request.
        if (currentUser.Identity == null || !currentUser.Identity.IsAuthenticated)
            return null; // Return null to indicate no authenticated user.

        // Extract the user's unique identifier (e.g., GUID or database ID) from claims.
        // ClaimTypes.NameIdentifier is a standard claim type for the user's unique ID.
        // The '!' (null-forgiving operator) asserts that FindFirst will not return null here,
        // assuming authentication ensures this claim's presence.
        var userId = currentUser.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        // Extract the user's email address from claims.
        // ClaimTypes.Email is a standard claim type for the user's email.
        var email = currentUser.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;

        // Extract all roles associated with the user from claims.
        // ClaimTypes.Role is a standard claim type for user roles.
        // FindAll returns an IEnumerable of all matching claims, which are then projected to their string values.
        var roles = currentUser.FindAll(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);

        // Create and return a new CurrentUser object encapsulating the extracted user details.
        return new CurrentUser(userId, email, roles);
    }
}
