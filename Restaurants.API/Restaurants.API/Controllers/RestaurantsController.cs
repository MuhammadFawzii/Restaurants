using System.Reflection.Metadata;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
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
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery()) ;
            return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RestaurantDto?>> GetRestaurantById([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]

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
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            var isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRestaurnt([FromBody] UpdateRestaurantCommand command, [FromRoute] int id)
        {
            if (command==null)
            {
                return BadRequest("Restaurant data is null.");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != command.Id)
            {
                return BadRequest("Id in the URL and body do not match.");
            }
            var isUpdated=await mediator.Send(command);
            if (isUpdated)
            {
                return NoContent();
            }
            return NotFound();

        }
    }
}
