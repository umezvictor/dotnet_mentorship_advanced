using AutoMapper;
using BLL.Abstractions;
using MediatR;
using Shared;

namespace BLL.Features.Categories.Update;
public sealed class UpdateCategoryCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; } = string.Empty;

}

public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper) :
    IRequestHandler<UpdateCategoryCommand, Response<string>>
{

    public async Task<Response<string>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(command.Id, cancellationToken);
        if (category is null)
        {
            return new Response<string>(ResponseMessage.CategoryNotFound, false);
        }

        await categoryRepository.UpdateAsync(mapper.Map(command, category), cancellationToken);
        return new Response<string>(ResponseMessage.CategoryUpdated, true);
    }
}
