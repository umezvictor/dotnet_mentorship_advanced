using FluentValidation;
namespace BLL.Features.Add;
public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
{
    public AddToCartCommandValidator()
    {

        RuleFor(p => p.Id)
        .GreaterThan(0).WithMessage("Id must be greater than 0"); ;


        RuleFor(p => p.Name).NotEmpty()
             .NotNull()
             .WithMessage("Name is required");


        RuleFor(p => p.Quantity)
        .GreaterThan(0).WithMessage("Quantity must be greater than 0");
    }

}

