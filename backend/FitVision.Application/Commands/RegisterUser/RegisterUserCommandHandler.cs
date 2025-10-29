using FitVision.Application.Exceptions;
using FitVision.Application.Interfaces;
using FitVision.Domain.Entities;
using MediatR;

namespace FitVision.Application.Commands.RegisterUser
{
    internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;

        public RegisterUserCommandHandler(IUserRepository users, IPasswordHasher hasher)
        {
            _users = users;
            _hasher = hasher;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken ct)
        {
            if (await _users.ExistsAsync(request.Email))
                throw new ValidationException("Email already registered.");

            var hash = _hasher.Hash(request.Password);
            var user = new User(request.Email, hash);
            await _users.AddAsync(user);
            return user.Id;
        }
    }
}
