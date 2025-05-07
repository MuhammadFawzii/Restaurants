using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantsService
    {
        Task<IEnumerable<RestaurantDto?>> GetAllRestaurantsAsync();
        Task <RestaurantDto?> GetRestaurantByIdAsynce(int id);

        Task<int> AddRestaurantAsynce(CreateRestaurantDto restaurantDto);
    }
}