using FitVision.Application.Commands.LoginUser;
using FitVision.Application.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitVision.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
           var id = await _mediator.Send(command);
           return CreatedAtAction(nameof(Register), new { id }, null);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserCommand command)
        {
            var token = _mediator.Send(command);
            return Ok(new { token });
        }
    }
}
