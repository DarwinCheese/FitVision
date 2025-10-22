using AutoMapper;
using MediatR;
using FitVision.Domain.Interfaces;
using FitVision.Domain.Entities;
using FitVision.Application.DTOs;
using Microsoft.Extensions.Logging;
using FitVision.Application.Exceptions;

namespace FitVision.Application.Commands.DeleteMeal;

public class DeleteMealHandler : IRequestHandler<DeleteMealCommand, Unit>
{
    private readonly IMealRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteMealHandler> _logger;

    public DeleteMealHandler(IMealRepository repo, IMapper mapper, ILogger<DeleteMealHandler> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing DeleteMealCommand: {MealId}", request.Id);

        try
        {
            await _repo.DeleteAsync(request.Id, cancellationToken);
            _logger.LogInformation("Deleted Meal successfully: {MealId}", request.Id);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new meal: {MealName}", request.Id);
            throw new DomainException("An error occurred while saving your meal. Please try again later.");
        }
        
    }
}