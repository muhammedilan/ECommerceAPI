using ECommerceAPI.Application.Repositories;
using FluentValidation;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.CreateProduct
{ 
    public class CreateProductCommandHandler(IProductWriteRepository _productWriteRepository, IValidator<CreateProductCommandRequest> _validator) : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository = _productWriteRepository;
        private readonly IValidator<CreateProductCommandRequest> _validator = _validator;

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Stock = request.Stock,
                Price = request.Price,
            });
            await _productWriteRepository.SaveAsync();

            return new();
        }
    }
}
