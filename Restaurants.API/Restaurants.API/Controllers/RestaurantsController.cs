using System.Reflection.Metadata;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery()) ;
            return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRestaurantById([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand commad)
        {
            if (commad == null)
            {
                return BadRequest("Restaurant data is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resatuarantId= await mediator.Send(commad);
            if (resatuarantId == 0)
            {
                return BadRequest("Failed to add restaurant.");
            }
            return CreatedAtAction(nameof(GetRestaurantById),new { id = resatuarantId }, commad);
        }
    }
}
