using FitVision.Application.Commands.PartialUpdateMeal;
using FitVision.Application.Exceptions;
using FitVision.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Meals.Commands.PartialUpdateMeal;

public class PartialUpdateMealCommandHandler : IRequestHandler<PartialUpdateMealCommand, Unit>
{
    private readonly IMealRepository _repo;
    private readonly ILogger<PartialUpdateMealCommandHandler> _logger;

    public PartialUpdateMealCommandHandler(IMealRepository repo, ILogger<PartialUpdateMealCommandHandler> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<Unit> Handle(PartialUpdateMealCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Partially updating meal with ID {MealId}", request.Id);

        var meal = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (meal == null)
        {
            _logger.LogWarning("Meal {MealId} not found", request.Id);
            throw new NotFoundException($"Meal with ID {request.Id} not found.");
        }

        if (request.Name is not null)
            meal.UpdateName(request.Name);

        if (request.Calories is not null)
            meal.UpdateCalories(request.Calories.Value);

        if (request.EatenAt is not null)
            meal.UpdateEatenAt(request.EatenAt.Value);

        if (request.Notes is not null)
            meal.UpdateNotes(request.Notes);

        await _repo.UpdateAsync(meal);

        _logger.LogInformation("Meal {MealId} partially updated successfully", request.Id);
        return Unit.Value;
    }
}
