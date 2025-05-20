using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishesForRestaurant;
using Restaurants.Application.Dishes.Commands.DeleteDishForRestaurant;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetAllDishForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

namespace Restaurants.API.Controllers;

[Route("api/restaurants/{restaurantId}/dishes")]
[ApiController]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        int dishId= await mediator.Send(command);
        return CreatedAtAction(nameof(GetDishByIdForRestaurant), new {restaurantId,dishId},null);
    }
    //GET {{Restaurants.API_HostAddress}}/api/restaurants/3/dishes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllDishesForRestaurant([FromRoute] int restaurantId)
    {
        var Dishes = await mediator.Send(new GetAllDishForRestaurantQuery(restaurantId));
        return Ok(Dishes);
    }
    [HttpGet("{dishId:int}")]
    public async Task<ActionResult<DishDto>> GetDishByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        var Dishe = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
        return Ok(Dishe);
    }
    //GET {{Restaurants.API_HostAddress}}/api/restaurants/3/dishes/3

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteDishesForRestaurant([FromRoute] int restaurantId)
    {
        await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
        return NoContent();
    }
    [HttpDelete("{dishId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteDishByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        await mediator.Send(new DeleteDishForRestaurantCommand(restaurantId,dishId));
        return NoContent();
    }

}
