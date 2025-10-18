using FluentValidation;

namespace BLL.Features.Categories.Update;
public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty()
             .NotNull()
             .WithMessage("Name is required");

        RuleFor(p => p.Id).NotEmpty()
            .NotNull()
            .WithMessage("Id is required");
    }

}