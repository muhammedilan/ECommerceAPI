using ECommerceAPI.Application.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler(IProductReadRepository _productReadRepository, IProductWriteRepository _productWriteRepository) : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository = _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository = _productWriteRepository;

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            product.Name = request.Name;
            product.Stock = request.Stock;
            product.Price = request.Price;

            await _productWriteRepository.SaveAsync();

            return new();
        }
    }
}
