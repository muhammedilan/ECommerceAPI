using ECommerceAPI.Application.DTOs.Product;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application.Services;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductWriteRepository _productWriteRepository, IProductReadRepository _productReadRepository, IFileService _fileService, IProductImageFileWriteRepository _productImageFileWriteRepository) : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository = _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository = _productReadRepository;
        private readonly IFileService _fileService = _fileService;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository = _productImageFileWriteRepository;

        [HttpGet]
        public IActionResult Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductDto model, [FromServices] IValidator<CreateProductDto> validator)
        {
            var validationResult = await validator.ValidateAsync(model);
            
            if (!validationResult.IsValid)
            {
                var modelStateDictionary = new ModelStateDictionary();

                foreach (ValidationFailure failure in validationResult.Errors)
                    modelStateDictionary.AddModelError(failure.PropertyName, failure.ErrorMessage);

                return ValidationProblem(modelStateDictionary);
            }

            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Stock = model.Stock,
                Price = model.Price,
            });
            await _productWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductDto model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);

            product.Name = model.Name;
            product.Stock = model.Stock;
            product.Price = model.Price;

            await _productWriteRepository.SaveAsync();
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            var datas = await _fileService.UploadAsync("resource/product-images", Request.Form.Files);

            List<ProductImageFile> productImageFiles = datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.path
            }).ToList();

            await _productImageFileWriteRepository.AddRangeAsync(productImageFiles);
            await _productImageFileWriteRepository.SaveAsync();

            return Ok();
        }
    };
}
