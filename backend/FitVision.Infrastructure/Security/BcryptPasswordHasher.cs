using FitVision.Application.Interfaces;

namespace FitVision.Infrastructure.Security
{
    internal sealed class BcryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
