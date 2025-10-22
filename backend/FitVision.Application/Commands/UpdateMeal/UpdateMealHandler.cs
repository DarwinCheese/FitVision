using AutoMapper;
using MediatR;
using FitVision.Domain.Interfaces;
using FitVision.Domain.Entities;
using FitVision.Application.DTOs;
using Microsoft.Extensions.Logging;
using FitVision.Application.Exceptions;

namespace FitVision.Application.Commands.UpdateMeal;

public class UpdateMealHandler : IRequestHandler<UpdateMealCommand, MealDto>
{
    private readonly IMealRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateMealHandler> _logger;

    public UpdateMealHandler(IMealRepository repo, IMapper mapper, ILogger<UpdateMealHandler> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<MealDto> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing UpdateMealCommand: {MealId}", request.Id);

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("Meal name cannot be empty.");

        try
        {
            var meal = new Meal
            {
                Name = request.Name,
                Calories = request.Calories,
                EatenAt = request.EatenAt,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            var added = await _repo.AddAsync(meal, cancellationToken);
            return _mapper.Map<MealDto>(added);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new meal: {MealName}", request.Name);
            throw new DomainException("An error occurred while saving your meal. Please try again later.");
        }
        
    }
}