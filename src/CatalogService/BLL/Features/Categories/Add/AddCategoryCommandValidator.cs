using FluentValidation;

namespace BLL.Features.Categories.Add;
public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty()
             .NotNull()
             .WithMessage("Name is required");
    }

}