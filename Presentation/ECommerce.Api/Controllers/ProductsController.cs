using ECommerceAPI.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService _productService) : ControllerBase
    {
        private readonly IProductService _productService = _productService;

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }
    }
}
