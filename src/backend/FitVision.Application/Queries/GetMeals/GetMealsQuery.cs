using MediatR;
using FitVision.Application.DTOs;

namespace FitVision.Application.Queries.GetMeals;

public record GetMealsQuery() : IRequest<IEnumerable<MealDto>>;