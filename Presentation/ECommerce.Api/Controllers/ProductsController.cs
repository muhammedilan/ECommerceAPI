using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Product.RemoveProduct;
using ECommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using ECommerceAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ECommerceAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerceAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator _mediator) : ControllerBase
    {
        private readonly IMediator _mediator = _mediator;

        [HttpGet]
        [EndpointDescription("Sayfa ve ürün sayısına göre ürünleri getir")]
        public async Task<ActionResult<GetAllProductQueryResponse>> GetAllProducts([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        [EndpointDescription("Product Id'e göre ürün getir")]
        public async Task<ActionResult<GetByIdProductQueryResponse>> GetByIdProduct([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [EndpointDescription("Ürün oluştur")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CreateProductCommandResponse>> CreateProduct([FromBody] CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        [EndpointDescription("Product Id'e göre ürün güncelle")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UpdateProductCommandResponse>> UpdateProduct([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        [HttpDelete("{Id}")]
        [EndpointDescription("Product Id'e göre ürün sil")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RemoveProductCommandResponse>> DeleteProduct([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }

        [HttpPost("images/upload")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [EndpointDescription("Product Id'e göre ürün resimleri yükle")]
        public async Task<ActionResult<UploadProductImageCommandResponse>> UploadProduct([FromQuery] string id)
        {
            var uploadProductImageCommandRequest = new UploadProductImageCommandRequest()
            {
                Id = id,
                Files = Request.Form.Files
            };

            UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("images/{Id}")]
        [EndpointDescription("Product Id'e göre ürün resimlerini getir")]
        public async Task<ActionResult<GetProductsImageQueryResponse>> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            List<GetProductsImageQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response);
        }

        [HttpDelete("{productId}/images/{imageId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [EndpointDescription("Product Id ve Image Id'e göre ürün resmini sil")]
        public async Task<ActionResult<RemoveProductImageCommandResponse>> DeleteProductImage([FromRoute] string imageId, string productId)
        {
            var removeProductImageCommandRequest = new RemoveProductImageCommandRequest()
            {
                ImageId = imageId,
                ProductId = productId
            };

            RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);

            if (response is null)
                return BadRequest();

            return Ok(response);
        }
    };
}
