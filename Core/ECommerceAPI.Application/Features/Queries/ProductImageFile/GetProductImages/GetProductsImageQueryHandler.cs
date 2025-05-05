using ECommerceAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductsImageQueryHandler(IProductReadRepository _productReadRepository) : IRequestHandler<GetProductImagesQueryRequest, List<GetProductsImageQueryResponse>>
    {
        private readonly IProductReadRepository _productReadRepository = _productReadRepository;

        public async Task<List<GetProductsImageQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            return product.ProductImageFiles.Select(p => new GetProductsImageQueryResponse()
            {
                Path = p.Path,
                FileName = p.FileName,
                ProductId = p.Id
            }).ToList();
        }
    }
}
