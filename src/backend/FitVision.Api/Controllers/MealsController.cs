using MediatR;
using Microsoft.AspNetCore.Mvc;
using FitVision.Application.DTOs;
using FitVision.Application.Commands.CreateMeal;
using FitVision.Application.Queries.GetMeals;

namespace FitVision.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MealsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var res = await _mediator.Send(new GetMealsQuery(), cancellationToken);
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMealCommand command, CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
    }
}