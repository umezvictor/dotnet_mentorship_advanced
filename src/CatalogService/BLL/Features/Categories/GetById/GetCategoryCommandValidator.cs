using FluentValidation;

namespace BLL.Features.Categories.GetById;
public class GetCategoryCommandValidator : AbstractValidator<GetCategoryQuery>
{
    public GetCategoryCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .NotNull()
        .GreaterThan(0).WithMessage("Id must be greater than 0");

    }

}