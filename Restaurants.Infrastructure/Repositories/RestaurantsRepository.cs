using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantsDbContext dbContext): IRestaurantsRepository
    {

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsFromDBAsync()
        {
            return await dbContext.Restaurants.Include(x=>x.Dishes). ToListAsync();
        }

        public async Task <Restaurant?> GetRestaurantByIdFromDBAsync(int id)
        {
            return await dbContext.Restaurants.Include(x=>x.Dishes).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<int> AddRestaurantToDBAsynce(Restaurant restaurant)
        {
            await dbContext.Restaurants.AddAsync(restaurant);
            await dbContext.SaveChangesAsync();
            int restaurantId = restaurant.Id;
            return restaurantId;
        }

        public async Task<bool> DeleteRestaurantFromDBAsynce(int id)
        {
            var restaurant = await dbContext.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return false;
            }
            dbContext.Restaurants.Remove(restaurant);
            await dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<int> SaveAsync()
        {
            int effectedRows = await dbContext.SaveChangesAsync();
            return effectedRows;

        }



    }
}
