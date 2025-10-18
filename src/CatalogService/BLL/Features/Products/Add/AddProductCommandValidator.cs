using FluentValidation;

namespace BLL.Features.Products.Add;
public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty()
             .NotNull()
             .MaximumLength(50)
             .WithMessage("Name is required, and must have a maximum of 50 charactuers");

        RuleFor(p => p.Price)
         .NotNull()
         .Must(price => price >= 0)
         .WithMessage("Price is required");

        RuleFor(p => p.Amount)
        .NotNull()
        .GreaterThan(0)
        .WithMessage("Amount is required and must be a positive integer.");

        RuleFor(p => p.CategoryId)
       .NotNull()
       .GreaterThan(0);
    }

}