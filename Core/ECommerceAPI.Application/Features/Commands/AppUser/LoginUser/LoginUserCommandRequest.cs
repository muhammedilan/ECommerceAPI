using MediatR;
using System.ComponentModel;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        [DefaultValue("admin")]
        public string UsernameOrEmail { get; set; }
        [DefaultValue("123")]
        public string Password { get; set; }
    }
}
