using System;

namespace DNFKit.Core.Dtos
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
