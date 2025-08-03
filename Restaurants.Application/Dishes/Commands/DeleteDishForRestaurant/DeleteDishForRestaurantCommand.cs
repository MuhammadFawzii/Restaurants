using MediatR;
namespace Restaurants.Application.Dishes.Commands.DeleteDishForRestaurant;

public class DeleteDishForRestaurantCommand:IRequest
{
    public DeleteDishForRestaurantCommand(int restaurantId,int dishId)
    {
        RestaurantId = restaurantId;
        DishId = dishId;
    }
    public int RestaurantId { get; }
    public int DishId { get; }
}
