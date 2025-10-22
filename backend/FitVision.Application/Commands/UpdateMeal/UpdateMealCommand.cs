using MediatR;

namespace FitVision.Application.Commands.UpdateMeal;

public record UpdateMealCommand(Guid Id, string Name, int Calories, DateTime EatenAt, string? Notes) : IRequest<Unit>;