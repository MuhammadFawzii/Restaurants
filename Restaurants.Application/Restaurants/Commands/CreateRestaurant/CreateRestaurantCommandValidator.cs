using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator:AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];

    public CreateRestaurantCommandValidator()
    {
        RuleFor(x=>x.Name)
            .NotEmpty()
            .WithMessage("The Name is Required")
            .Length(3,100);
        RuleFor(x => x.Category)
            .Custom((value, context) =>
            {
                if (!validCategories.Contains(value))
                {
                    context.AddFailure("Category", "Invalid category. Please choose from the valid categories.");
                }
            });
        //RuleFor(x => x.Category)
        //    .Must(Category=>validCategories.Contains(Category))
        //    .WithMessage("Invalid category. Please choose from the valid categories.");
            
        RuleFor(x=>x.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid postal code (XX-XXX).");
        RuleFor(x=>x.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");


    }

}
