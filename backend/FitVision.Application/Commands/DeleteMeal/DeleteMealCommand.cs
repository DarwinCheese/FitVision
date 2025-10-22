using MediatR;

namespace FitVision.Application.Commands.DeleteMeal;

public record DeleteMealCommand(Guid Id) : IRequest<Unit>;