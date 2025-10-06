using FluentValidation;
namespace BLL.Features.Delete;
public class DeleteItemFromCartCommandValidator : AbstractValidator<DeleteItemFromCartCommand>
{
    public DeleteItemFromCartCommandValidator()
    {

        RuleFor(p => p.Id)
        .GreaterThan(0).WithMessage("Invalid cart item"); ;


    }

}

