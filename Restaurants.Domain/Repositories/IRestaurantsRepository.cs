using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        public Task<IEnumerable<Restaurant>> GetAllRestaurantsFromDBAsync();
        public Task<Restaurant?> GetRestaurantByIdFromDBAsync(int id);
        public Task<int> AddRestaurantToDBAsynce(Restaurant restaurant);




    }
}
