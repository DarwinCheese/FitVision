using FitVision.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FitVision.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }

        private User() { }

        public User(string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email cannot be empty.");
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("Password cannot be empty.");

            Email = email.ToLowerInvariant();
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }

        public void ChangePassword(string newHash)
        {
            PasswordHash = newHash;
        }
    }
}
