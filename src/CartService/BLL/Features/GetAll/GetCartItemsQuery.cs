using AutoMapper;
using CartService.DAL.Database.Repository;
using DAL.Dto;
using MediatR;

namespace BLL.Features.GetAll;


public sealed class GetCartItemsQuery : IRequest<List<CartDto>>
{

}

public class GetCartItemsQueryHandler(ICartRepository cartRepository, IMapper mapper) :
    IRequestHandler<GetCartItemsQuery, List<CartDto>>
{

    public async Task<List<CartDto>> Handle(GetCartItemsQuery command, CancellationToken cancellationToken)
    {
        var cartItems = await cartRepository.GetItemsAsync(cancellationToken);
        return mapper.Map<List<CartDto>>(cartItems);
    }
}