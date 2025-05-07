using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await restaurantsService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRestaurantById([FromRoute] int id)
        {
            var restaurant = await restaurantsService.GetRestaurantByIdAsynce(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> AddRestaurant([FromBody] CreateRestaurantDto restaurantDto)
        {
            if (restaurantDto == null)
            {
                return BadRequest("Restaurant data is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resatuarantId= await restaurantsService.AddRestaurantAsynce(restaurantDto);
            if (resatuarantId == 0)
            {
                return BadRequest("Failed to add restaurant.");
            }
            return CreatedAtAction(nameof(GetRestaurantById),new { id = resatuarantId },restaurantDto);
        }
    }
}
