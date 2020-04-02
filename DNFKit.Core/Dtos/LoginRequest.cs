using System;

namespace DNFKit.Core.Dtos
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class TokenRequest
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
