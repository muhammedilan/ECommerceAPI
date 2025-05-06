namespace ECommerceAPI.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<DTOs.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenExpirationMinutes);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
