using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> _userManager, SignInManager<Domain.Entities.Identity.AppUser> _signInManager, ITokenHandler _tokenHandler) : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager = _userManager;
        private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager = _signInManager;
        private readonly ITokenHandler _tokenHandler = _tokenHandler;

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user is null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user is null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(5);
                return new LoginUserSuccessCommandResponse()
                {
                    Token = token,
                };
            }

            return new LoginUserErrorCommandResponse()
            {
                Message = "Giriş bilgileri yanlış"
            };
        }
    }
}
