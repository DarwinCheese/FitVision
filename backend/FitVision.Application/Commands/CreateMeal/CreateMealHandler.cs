using AutoMapper;
using MediatR;
using FitVision.Domain.Entities;
using FitVision.Application.DTOs;
using Microsoft.Extensions.Logging;
using FitVision.Application.Exceptions;
using FitVision.Application.Interfaces;

namespace FitVision.Application.Commands.CreateMeal;

internal sealed class CreateMealHandler : IRequestHandler<CreateMealCommand, MealDto>
{
    private readonly IMealRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateMealHandler> _logger;
    private readonly ICurrentUserService _currentUserService;

    public CreateMealHandler(IMealRepository repo, IMapper mapper, ILogger<CreateMealHandler> logger, ICurrentUserService currentUserService)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<MealDto> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing CreateMealCommand: {MealName}", request.Name);

        var userId = _currentUserService.UserId
            ?? throw new UnauthorizedAccessException("User not logged in");

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("Meal name cannot be empty.");

        try
        {
            var meal = new Meal(request.Name, request.Calories, userId, request.EatenAt, request.Notes);

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