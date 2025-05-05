using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage
{
    public class RemoveProductImageCommandRequest : IRequest<RemoveProductImageCommandResponse>
    {
        public string ProductId { get; set; }
        public string ImageId { get; set; }
    }
}
