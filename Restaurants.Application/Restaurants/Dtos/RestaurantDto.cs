using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public List<DishDto> Dishes { get; set; } = [];

    //this manual mapping is not necessary if you use a library like AutoMapper
    //public static RestaurantDto FromEntity(Restaurant restaurant)
    //{
    //    return new RestaurantDto
    //    {
    //        Id = restaurant.Id,
    //        Name = restaurant.Name,
    //        Description = restaurant.Description,
    //        Category = restaurant.Category,
    //        HasDelivery = restaurant.HasDelivery,
    //        City = restaurant.Address?.City,
    //        Street = restaurant.Address?.Street,
    //        PostalCode = restaurant.Address?.PostalCode,
    //        Dishes = restaurant.Dishes.Select(DishDto.FromEntity).ToList()
    //    };
    //}
}
