using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
{
    public async Task <int> AddDishesToDBAsynce(Dish dish)
    {
        await dbContext.Dishes.AddAsync(dish);
        await dbContext.SaveChangesAsync();
        return dish.Id;
    }
    public async Task<IEnumerable<Dish>> GetAllDishesForRestaurantFromDBAsync(int restuarntId)
    {
        var dishes = await dbContext.Dishes
            .Where(d => d.RestaurantId == restuarntId).ToListAsync() ;
        return dishes;
    }
    public async Task<Dish?> GetDishByIdForRestaurantFromDBAsync(int restuarntId, int dishId)
    {
        var dish= await dbContext.Dishes.
            Where(d=>d.RestaurantId==restuarntId&& d.Id==dishId).FirstOrDefaultAsync();
        return dish;
    }

    public async Task<bool> DeleteDishesForRestaurantByIdFromDBAsync(int restaurantId)
    {

        IEnumerable<Dish> dishes= await dbContext.Dishes.Where(d => d.RestaurantId==restaurantId).ToListAsync();
        dbContext.Dishes.RemoveRange(dishes);
        int c= await dbContext.SaveChangesAsync();
        bool check = (c > 0) ? true : false;
        return check;
                
    }
    public async Task<bool> DeleteDishByIdForRestaurantByIdFromDBAsync(int restaurantId,int dishId)
    {
        var dish =await dbContext.Dishes
            .Where(d=>d.RestaurantId == restaurantId && d.Id == dishId).FirstOrDefaultAsync();
        if (dish != null)
        {
            dbContext.Dishes.Remove(dish);
            int c = await dbContext.SaveChangesAsync();
            bool check = (c > 0) ? true : false;
            return check;
        }
        return false;

    }
    
}
