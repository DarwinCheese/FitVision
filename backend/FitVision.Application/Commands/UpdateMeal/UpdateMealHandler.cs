using MediatR;
using Microsoft.Extensions.Logging;
using FitVision.Application.Exceptions;
using FitVision.Application.Interfaces;

namespace FitVision.Application.Commands.UpdateMeal;

internal sealed class UpdateMealHandler : IRequestHandler<UpdateMealCommand, Unit>
{
    private readonly IMealRepository _repo;
    private readonly ILogger<UpdateMealHandler> _logger;

    public UpdateMealHandler(IMealRepository repo, ILogger<UpdateMealHandler> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing UpdateMealCommand: {MealId}", request.Id);

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("Meal name cannot be empty.");

        var meal = await _repo.GetByIdAsync(request.Id);
        if (meal == null)
        {
            _logger.LogWarning("Meal with ID {MealId} not found", request.Id);
            throw new NotFoundException($"Meal with ID {request.Id} not found.");
        }

        // Update fields
        meal.Update(request.Name, request.Calories, request.EatenAt, request.Notes);

        await _repo.UpdateAsync(meal);

        _logger.LogInformation("Meal {MealId} updated successfully", request.Id);
        return Unit.Value;

    }
}