using FluentValidation;
namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandValidator: AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(x=>x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters");
        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price is required")
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
        RuleFor(x => x.KiloCalories)
            .NotEmpty()
            .WithMessage("KiloCalories is required")
            .GreaterThan(0)
            .WithMessage("KiloCalories must be greater than 0");

    }
}
