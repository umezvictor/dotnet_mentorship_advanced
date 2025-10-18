using BLL.Abstractions;
using MediatR;
using Shared;

namespace BLL.Features.Categories.Delete;
public sealed class DeleteCategoryCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
}

public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository) :
    IRequestHandler<DeleteCategoryCommand, Response<string>>
{

    public async Task<Response<string>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(command.Id, cancellationToken);
        if (category == null)
        {
            return new Response<string>(ResponseMessage.CategoryNotFound, false);
        }
        await categoryRepository.DeleteAsync(category, cancellationToken);
        return new Response<string>(ResponseMessage.CategoryDeleted, true);
    }
}
