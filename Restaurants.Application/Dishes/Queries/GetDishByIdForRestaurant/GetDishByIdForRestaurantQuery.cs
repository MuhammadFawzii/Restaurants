using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQuery : IRequest<DishDto>
{
    public GetDishByIdForRestaurantQuery( int restuarantId,int dishId)
    {
        RestaurantId = restuarantId;
        DishId = dishId;

    }
    public int RestaurantId { get; set; }
    public int DishId { get; set; }
}
