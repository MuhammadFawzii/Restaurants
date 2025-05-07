namespace Restaurants.Infrastructure.Seaders
{
    //must be public
    public interface IRestaurantSeader
    {
        Task Seed();
    }
}