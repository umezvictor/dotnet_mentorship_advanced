using AutoMapper;
using BLL.Abstractions;
using MediatR;
using Shared;
using Shared.Dto;

namespace BLL.Features.Categories.GetAll;
public sealed class GetCategoriesQuery : IRequest<Response<List<CategoryDto>>>
{
}
public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) :
    IRequestHandler<GetCategoriesQuery, Response<List<CategoryDto>>>
{
    public async Task<Response<List<CategoryDto>>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllCategoriesAsync(cancellationToken);
        return new Response<List<CategoryDto>>(mapper.Map<List<CategoryDto>>(categories), ResponseMessage.Success);
    }
}