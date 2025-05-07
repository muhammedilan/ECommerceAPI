using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController(IMediator _mediator) : ControllerBase
    {
        private readonly IMediator _mediator = _mediator;

        [HttpPost]
        [EndpointDescription("Kullanıcı oluştur / Kayıt Ol")]
        public async Task<ActionResult<CreateUserCommandResponse>> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
    }
}
