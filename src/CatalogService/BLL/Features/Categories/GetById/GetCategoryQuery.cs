using AutoMapper;
using BLL.Abstractions;
using MediatR;
using Shared;
using Shared.Dto;

namespace BLL.Features.Categories.GetById;
public sealed class GetCategoryQuery : IRequest<Response<CategoryDto>>
{
    public int Id { get; set; }
}
public class GetCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) :
    IRequestHandler<GetCategoryQuery, Response<CategoryDto>>
{
    public async Task<Response<CategoryDto>> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(query.Id, cancellationToken);
        if (category is null)
        {
            return new Response<CategoryDto>(ResponseMessage.CategoryNotFound, false);
        }
        return new Response<CategoryDto>(mapper.Map<CategoryDto>(category), ResponseMessage.Success);
    }
}