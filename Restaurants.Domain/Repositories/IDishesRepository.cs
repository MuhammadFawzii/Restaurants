using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        public Task <int> AddDishesToDBAsynce(Dish dish);
        public Task<IEnumerable<Dish>> GetAllDishesForRestaurantFromDBAsync(int restuarntId);

        public Task<Dish?> GetDishByIdForRestaurantFromDBAsync(int restuarntId,int dishId);
        public Task<bool> DeleteDishesForRestaurantByIdFromDBAsync(int restaurantId);

        public Task<bool> DeleteDishByIdForRestaurantByIdFromDBAsync(int restaurantId, int dishId);

    }
}
