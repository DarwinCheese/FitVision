using MediatR;
using Microsoft.AspNetCore.Mvc;
using FitVision.Application.DTOs;
using FitVision.Application.Commands.CreateMeal;
using FitVision.Application.Queries.GetMeals;
using FitVision.Application.Queries.GetMealById;
using FitVision.Application.Commands.DeleteMeal;
using FitVision.Application.Commands.UpdateMeal;

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
    public async Task<IActionResult> GetMeal(CancellationToken cancellationToken)
    {
        var res = await _mediator.Send(new GetMealsQuery(), cancellationToken);
        return Ok(res);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMealById(Guid id, CancellationToken cancellationToken)
    {
        var res = await _mediator.Send(new GetMealByIdQuery(id), cancellationToken);
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMeal([FromBody] UpdateMealCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMeal([FromBody] DeleteMealCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMeal([FromBody] CreateMealCommand command, CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetMealById), new { id = dto.Id }, dto);
    }
}