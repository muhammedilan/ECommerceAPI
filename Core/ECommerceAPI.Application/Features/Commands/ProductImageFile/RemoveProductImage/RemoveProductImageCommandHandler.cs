using ECommerceAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage
{
    public class RemoveProductImageCommandHandler(IProductReadRepository _productReadRepository, IProductWriteRepository _productWriteRepository) : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository = _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository = _productWriteRepository;

        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.ProductId));

            Domain.Entities.ProductImageFile? productImageFile = product.ProductImageFiles
                .FirstOrDefault(pi => pi.Id == Guid.Parse(request.ImageId));

            if (productImageFile is not null)
                product.ProductImageFiles.Remove(productImageFile);

            await _productWriteRepository.SaveAsync();

            return new();
        }
    }
}
