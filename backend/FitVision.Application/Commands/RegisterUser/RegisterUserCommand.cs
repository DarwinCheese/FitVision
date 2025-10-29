using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVision.Application.Commands.RegisterUser
{
    public record RegisterUserCommand(string Email, string Password) : IRequest<Guid>;
}
