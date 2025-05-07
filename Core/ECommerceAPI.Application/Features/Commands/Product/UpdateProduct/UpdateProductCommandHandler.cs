using ECommerceAPI.Application.Repositories;
using FluentValidation;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler(IProductReadRepository _productReadRepository, IProductWriteRepository _productWriteRepository, IValidator<UpdateProductCommandRequest>  _validator) : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository = _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository = _productWriteRepository;
        private readonly IValidator<UpdateProductCommandRequest> _validator = _validator; 

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            product.Name = request.Name;
            product.Stock = request.Stock;
            product.Price = request.Price;

            await _productWriteRepository.SaveAsync();

            return new();
        }
    }
}
