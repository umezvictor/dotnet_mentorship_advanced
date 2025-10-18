using FluentValidation;
namespace BLL.Features.Delete;
public class DeleteItemFromCartCommandValidator : AbstractValidator<DeleteItemFromCartCommand>
{
    public DeleteItemFromCartCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .NotNull()
        .GreaterThan(0).WithMessage("Id must be greater than 0");


    }

}

