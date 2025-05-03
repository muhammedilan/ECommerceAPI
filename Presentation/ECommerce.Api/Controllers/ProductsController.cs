using ECommerceAPI.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductWriteRepository _productWriteRepository, IProductReadRepository _productReadRepository) : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository = _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository = _productReadRepository;
    }
}
