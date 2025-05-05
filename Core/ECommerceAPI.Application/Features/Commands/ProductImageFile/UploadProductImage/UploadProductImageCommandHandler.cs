using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler(IStorageService _storageService, IProductReadRepository _productReadRepository, IProductImageFileWriteRepository _productImageFileWriteRepository) : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        private readonly IStorageService _storageService = _storageService;
        private readonly IProductReadRepository _productReadRepository = _productReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository = _productImageFileWriteRepository;

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var datas = await _storageService.UploadAsync("resource/product-images", request.Files);
            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            List<Domain.Entities.ProductImageFile> productImageFiles = datas.Select(d => new Domain.Entities.ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Domain.Entities.Product>() { product }
            }).ToList();

            await _productImageFileWriteRepository.AddRangeAsync(productImageFiles);
            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
