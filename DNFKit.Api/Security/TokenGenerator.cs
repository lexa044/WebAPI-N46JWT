using System;
using System.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using DNFKit.Core.Dtos;

namespace DNKit.Api.Security
{
    internal static class TokenGenerator
    {
        public static TokenResponse GenerateTokenJwt(TokenRequest request)
        {
            //TODO: appsetting for Demo JWT - protect correctly this settings
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity 
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.PrimarySid, request.Id.ToString()),
                new Claim(ClaimTypes.Name, request.Username),
                //new Claim(ClaimTypes.Role, request.Role)
            });

            TokenResponse response = new TokenResponse();
            response.ExpiresIn = DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime));
            // create token to the user 
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: response.ExpiresIn,
                signingCredentials: signingCredentials);

            response.Token = tokenHandler.WriteToken(jwtSecurityToken);

            return response;
        }
    }
}