using MediatR;
using FitVision.Application.DTOs;

namespace FitVision.Application.Commands.UpdateMeal;

public record UpdateMealCommand(Guid Id, string? Name, int? Calories, DateTime? EatenAt, string? Notes, DateTime? CreatedAt) : IRequest<MealDto>;