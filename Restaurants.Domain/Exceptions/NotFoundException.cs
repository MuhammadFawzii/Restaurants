namespace Restaurants.Domain.Exceptions;

public class NotFoundException(string resourceType, string resourceIdentifier) 
    :Exception($"{resourceType} with this id :{resourceIdentifier} doesn't exist")
{


}
