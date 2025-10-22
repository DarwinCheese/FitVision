using MediatR;
using FitVision.Application.DTOs;

namespace FitVision.Application.Commands.CreateMeal;

public record UpdateMealCommand(string Name, int Calories, DateTime EatenAt, string? Notes, DateTime CreatedAt, Guid UserId) : IRequest<MealDto>;