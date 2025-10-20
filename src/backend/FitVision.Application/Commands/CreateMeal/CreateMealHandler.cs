using AutoMapper;
using MediatR;
using FitVision.Domain.Interfaces;
using FitVision.Domain.Entities;
using FitVision.Application.DTOs;

namespace FitVision.Application.Commands.CreateMeal;

public class CreateMealHandler : IRequestHandler<CreateMealCommand, MealDto>
{
    private readonly IMealRepository _repo;
    private readonly IMapper _mapper;

    public CreateMealHandler(IMealRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<MealDto> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        var meal = new Meal
        {
            Name = request.Name,
            Calories = request.Calories,
            EatenAt = request.EatenAt,
            Notes = request.Notes
        };

        var added = await _repo.AddAsync(meal, cancellationToken);
        return _mapper.Map<MealDto>(added);
    }
}