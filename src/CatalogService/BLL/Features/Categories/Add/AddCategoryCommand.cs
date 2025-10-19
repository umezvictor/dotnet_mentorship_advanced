using AutoMapper;
using BLL.Abstractions;
using Domain.Entities;
using MediatR;
using Shared;

namespace BLL.Features.Categories.Add;
public sealed class AddCategoryCommand : IRequest<Response<string>>
{
    public string Name { get; set; }
    public string Image { get; set; } = string.Empty;
    public string ParentCategory { get; set; } = string.Empty;

}

public class AddCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper) :
    IRequestHandler<AddCategoryCommand, Response<string>>
{
    public async Task<Response<string>> Handle(AddCategoryCommand command, CancellationToken cancellationToken)
    {
        await categoryRepository.CreateAsync(mapper.Map<Category>(command), cancellationToken);
        return new Response<string>(ResponseMessage.CategoryAdded, true);
    }
}
