using System;

namespace DNFKit.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSeed { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
