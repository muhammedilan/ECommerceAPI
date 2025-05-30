﻿namespace ECommerceAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccessToken(int minute);
        string CreateRefreshToken();
    }
}
