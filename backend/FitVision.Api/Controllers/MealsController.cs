using FitVision.Application.Commands.CreateMeal;
using FitVision.Application.Commands.DeleteMeal;
using FitVision.Application.Commands.PartialUpdateMealCommand;
using FitVision.Application.Commands.UpdateMeal;
using FitVision.Application.DTOs;
using FitVision.Application.Queries.GetMealById;
using FitVision.Application.Queries.GetMeals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateMeal(Guid Id, [FromBody] UpdateMealCommand command, CancellationToken cancellationToken)
    {
        if (Id != command.Id)
            return BadRequest();

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMeal(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteMealCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMeal([FromBody] CreateMealCommand command, CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetMealById), new { id = dto.Id }, dto);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> PatchMeal(Guid id, PartialUpdateMealCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }
}