using BLL.Features.Categories.Delete;
using FluentValidation;

namespace BLL.Features.Categories.Add;
public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {

        RuleFor(p => p.Id)
            .NotEmpty()
            .NotNull()
        .GreaterThan(0).WithMessage("Id must be greater than 0");

    }

}