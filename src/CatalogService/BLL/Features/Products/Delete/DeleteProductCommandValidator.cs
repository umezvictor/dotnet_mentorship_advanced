using BLL.Features.Products.Delete;
using FluentValidation;

namespace BLL.Features.Products.Add;
public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {

        RuleFor(p => p.Id)
            .NotEmpty()
            .NotNull()
        .GreaterThan(0).WithMessage("Id must be greater than 0");

    }

}