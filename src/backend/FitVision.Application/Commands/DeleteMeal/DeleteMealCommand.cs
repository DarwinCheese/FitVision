using MediatR;
using FitVision.Application.DTOs;

namespace FitVision.Application.Commands.DeleteMeal;

public record DeleteMealCommand(Guid Id) : IRequest<MealDto>;