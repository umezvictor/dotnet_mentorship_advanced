using FluentValidation;

namespace BLL.Features.Products.GetById;
public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .NotNull()
        .GreaterThan(0).WithMessage("Id must be greater than 0");

    }

}