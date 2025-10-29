using FitVision.Application.Exceptions;
using FitVision.Application.Interfaces;
using MediatR;

namespace FitVision.Application.Commands.LoginUser
{
    internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtTokenService _jwt;

        public LoginUserCommandHandler(IUserRepository users, IPasswordHasher hasher, IJwtTokenService jwt)
        {
            _users = users;
            _hasher = hasher;
            _jwt = jwt;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken ct)
        {
            var user = await _users.GetByEmailAsync(request.Email)
                       ?? throw new NotFoundException("Invalid credentials");

            if (!_hasher.Verify(request.Password, user.PasswordHash))
                throw new ValidationException("Invalid credentials");

            return _jwt.GenerateToken(user);
        }
    }
}
