using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seaders;

internal class RestaurantSeader(RestaurantsDbContext DbContextRestuarnt): IRestaurantSeader
{
    public async Task Seed()
    {
        if ( await DbContextRestuarnt.Database.CanConnectAsync())
        {
            if (!DbContextRestuarnt.Restaurants.Any())
            {
                var resturants = GetRestaurants();
                await DbContextRestuarnt.Restaurants.AddRangeAsync(resturants);
                await DbContextRestuarnt.SaveChangesAsync();
            }
            if(!DbContextRestuarnt.Roles.Any())
            {
                var roles = GetRoles();
                await DbContextRestuarnt.Roles.AddRangeAsync(roles);
                await DbContextRestuarnt.SaveChangesAsync();
            }
        }
       

    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = [
            new IdentityRole("Admin"){
                NormalizedName=UserRoles.Admin.ToUpper()
            },
            new IdentityRole("User"){
                NormalizedName=UserRoles.User.ToUpper()
            },
            new IdentityRole("Owner"){
                NormalizedName = UserRoles.User.ToUpper()
            }
        ];
        return roles;
    }
    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants = [
             new()
             {
                 Name = "KFC",
                 Category = "Fast Food",
                 Description =
                     "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                 ContactEmail = "contact@kfc.com",
                 HasDelivery = true,
                 Dishes =
                 [
                     new ()
                     {
                         Name = "Nashville Hot Chicken",
                         Description = "Nashville Hot Chicken (10 pcs.)",
                         Price = 10.30M,
                     },

                     new ()
                     {
                         Name = "Chicken Nuggets",
                         Description = "Chicken Nuggets (5 pcs.)",
                         Price = 5.30M,
                     },
                 ],
                 Address = new ()
                 {
                     City = "London",
                     Street = "Cork St 5",
                     PostalCode = "WC2N 5DU"
                 }
             },
             new ()
             {
                 Name = "McDonald",
                 Category = "Fast Food",
                 Description =
                     "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                 ContactEmail = "contact@mcdonald.com",
                 HasDelivery = true,
                 Address = new Address()
                 {
                     City = "London",
                     Street = "Boots 193",
                     PostalCode = "W1F 8SR"
                 }
             }
        ];
        return restaurants;
    }
}
