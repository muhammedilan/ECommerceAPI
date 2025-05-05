using MediatR;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImagesQueryRequest : IRequest<List<GetProductsImageQueryResponse>>
    {
        public string Id { get; set; }
    }
}
