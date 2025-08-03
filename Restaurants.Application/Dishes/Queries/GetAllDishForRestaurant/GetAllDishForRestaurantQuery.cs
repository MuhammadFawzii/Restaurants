using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetAllDishForRestaurant;

public class GetAllDishForRestaurantQuery:IRequest<IEnumerable<DishDto>>
{
    public GetAllDishForRestaurantQuery(int restaurantId)
    {
        RestaurantId=restaurantId;
    }
    public int RestaurantId { get; set; }
}
