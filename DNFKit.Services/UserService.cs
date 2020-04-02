using System;
using System.Security.Cryptography;
using System.Text;

using DNFKit.Core;
using DNFKit.Core.Dtos;
using DNFKit.Core.Models;
using DNFKit.Core.Repositories;
using DNFKit.Core.Services;

namespace DNFKit.Services
{
    public class UserService : IUserService
    {
        private readonly IDalSession _session;
        private readonly IUserRepository _repository;

        public UserService(IDalSession session, IUserRepository repository)
        {
            _session = session;
            _repository = repository;
        }

        public LoginResponse Authenticate(LoginRequest request, Func<TokenRequest, TokenResponse> tokenGenerator)
        {
            LoginResponse response = new LoginResponse();
            User model = _repository.FindByUsername(request.Username);
            if(null != model)
            {
                string passwordHash = EncryptPassword(request.Password, model.PasswordSeed);
                if (passwordHash == model.PasswordHash)
                {
                    TokenRequest tokenRequest = new TokenRequest();
                    tokenRequest.Id = model.Id;
                    tokenRequest.Username = request.Username;
                    tokenRequest.Role = "";

                    TokenResponse tokenResponse = tokenGenerator.Invoke(tokenRequest);
                    model.Token = tokenResponse.Token;
                    model.ExpiresIn = tokenResponse.ExpiresIn;

                    _repository.Update(model);

                    response.Id = model.Id;
                    response.ExpiresIn = tokenResponse.ExpiresIn;
                    response.Token = tokenResponse.Token;

                    _session.GetUnitOfWork().CommitChanges();
                }
            }

            return response;
        }

        public LoginResponse FetchIdentity(int id)
        {
            LoginResponse response = new LoginResponse();
            User model = _repository.FindById(id);
            response.Id = model.Id;
            response.Token = model.Token;
            response.ExpiresIn = model.ExpiresIn;

            return response;
        }

        private string EncryptPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", salt, password);
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }
    }
}
