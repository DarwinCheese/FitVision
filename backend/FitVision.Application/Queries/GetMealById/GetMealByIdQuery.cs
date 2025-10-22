using MediatR;
using FitVision.Application.DTOs;

namespace FitVision.Application.Queries.GetMealById;

public record GetMealByIdQuery(Guid Id) : IRequest<MealDto>;