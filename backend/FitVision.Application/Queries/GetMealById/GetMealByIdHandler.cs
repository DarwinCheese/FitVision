using AutoMapper;
using MediatR;
using FitVision.Application.DTOs;
using FitVision.Application.Interfaces;
using FitVision.Application.Exceptions;

namespace FitVision.Application.Queries.GetMealById;

public class GetMealByIdHandler : IRequestHandler<GetMealByIdQuery, MealDto>
{
    private readonly IMealRepository _repo;
    private readonly IMapper _mapper;

    public GetMealByIdHandler(IMealRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<MealDto> Handle(GetMealByIdQuery request, CancellationToken cancellationToken)
    {
        var meal = await _repo.GetByIdAsync(request.Id, cancellationToken);

        return meal == null ? throw new NotFoundException("Meal not found") : _mapper.Map<MealDto>(meal);
    }
}