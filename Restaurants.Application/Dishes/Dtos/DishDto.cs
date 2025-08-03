using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos;

public class DishDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }

    /// <summary>
    /// Converts a Dish entity to a DishDto object.
    /// This method ensures separation of concerns by isolating the data transfer logic
    /// from the domain entity. It adheres to the DTO pattern, which is used to transfer
    /// data between application layers while maintaining encapsulation and avoiding
    /// direct exposure of domain entities.
    /// </summary>
    //public static DishDto FromEntity(Dish dish)
    //{
    //    return new DishDto
    //    {
    //        Id = dish.Id,
    //        Name = dish.Name,
    //        Description = dish.Description,
    //        Price = dish.Price,
    //        KiloCalories = dish.KiloCalories
    //    };
    //}
}
