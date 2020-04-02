using System;

using DNFKit.Core.Dtos;

namespace DNFKit.Core.Services
{
    public interface IUserService
    {
        LoginResponse Authenticate(LoginRequest request, Func<TokenRequest, TokenResponse> tokenGenerator);
        LoginResponse FetchIdentity(int id);
    }
}
