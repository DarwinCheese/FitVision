using AutoMapper;
using MediatR;
using FitVision.Application.DTOs;
using FitVision.Application.Interfaces;

namespace FitVision.Application.Queries.GetMeals;

public class GetMealsHandler : IRequestHandler<GetMealsQuery, IEnumerable<MealDto>>
{
    private readonly IMealRepository _repo;
    private readonly IMapper _mapper;

    public GetMealsHandler(IMealRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MealDto>> Handle(GetMealsQuery request, CancellationToken cancellationToken)
    {
        var all = await _repo.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<MealDto>>(all);
    }
}