using FluentValidation;

namespace BLL.Features.Products.Update;
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty()
             .NotNull()
             .WithMessage("Name is required");

        RuleFor(p => p.Id).NotEmpty()
            .NotNull()
            .GreaterThan(0).WithMessage("Id must be greater than 0");

        RuleFor(p => p.Price)
        .NotEmpty()
        .NotNull()
        .Must(price => price >= 0 && decimal.Round(price, 2) == price)
        .WithMessage("Price must be a non-negative number with up to two decimal places.");

        RuleFor(p => p.Amount)
        .NotNull()
        .GreaterThan(0)
        .WithMessage("Amount is required and must be a positive integer.");
    }

}

