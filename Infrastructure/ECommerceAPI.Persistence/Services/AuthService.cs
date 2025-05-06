using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services
{
    public class AuthService(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, ITokenHandler _tokenHandler) : IAuthService
    {
        private readonly UserManager<AppUser> _userManager = _userManager;
        private readonly SignInManager<AppUser> _signInManager = _signInManager;
        private readonly ITokenHandler _tokenHandler = _tokenHandler;

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenExpirationMinutes)
        {
            var user = await _userManager.FindByNameAsync(usernameOrEmail) ?? await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user is null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(minute: accessTokenExpirationMinutes);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = DateTime.UtcNow.AddDays(7);
                
                await _userManager.UpdateAsync(user);

                return token;
            }

            throw new AuthenticationErrorException();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user is null)
                throw new NotFoundUserException();

            if (user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(minute: 15);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = DateTime.UtcNow.AddDays(7);
                await _userManager.UpdateAsync(user);

                return token;
            }
            else
            {
                throw new Exception("Refresh Token has expired");
            }
        }
    }
}
