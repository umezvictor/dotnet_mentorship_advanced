using AutoMapper;
using CartService.BLL.Abstractions;
using MediatR;
using Shared;
using Shared.Dto;

namespace BLL.Features.GetAll;


public sealed class GetCartItemsQuery : IRequest<Response<List<CartDto>>>
{

}

public class GetCartItemsQueryHandler(ICartRepository cartRepository, IMapper mapper) :
    IRequestHandler<GetCartItemsQuery, Response<List<CartDto>>>
{

    public async Task<Response<List<CartDto>>> Handle(GetCartItemsQuery command, CancellationToken cancellationToken)
    {
        var cartItems = await cartRepository.GetItemsAsync();
        return new Response<List<CartDto>>(mapper.Map<List<CartDto>>(cartItems), ResponseMessage.ItemsFetched);
    }
}