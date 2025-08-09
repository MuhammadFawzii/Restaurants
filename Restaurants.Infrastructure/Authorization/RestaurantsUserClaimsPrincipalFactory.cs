

using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Authorization;

public class RestaurantsUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager, IOptions <IdentityOptions> options):
    UserClaimsPrincipalFactory<ApplicationUser,IdentityRole> (userManager,roleManager,options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var id = await GenerateClaimsAsync(user);
        if(user.Nationality != null) 
            id.AddClaim(new Claim ("Nationality",user.Nationality));
            
        if(user.BirthDate != null)
            id.AddClaim(new Claim("BirthDate", user.BirthDate.Value.ToString("yyyy-MM-dd")));

        return new ClaimsPrincipal(id);
    }
}
