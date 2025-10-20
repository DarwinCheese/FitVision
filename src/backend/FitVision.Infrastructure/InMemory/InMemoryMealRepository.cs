using FitVision.Domain.Entities;
using FitVision.Domain.Interfaces;
using System.Collections.Concurrent; 
using System.Linq;

namespace FitVision.Infrastructure.InMemory;

public class InMemoryMealRepository : IMealRepository
{
    private readonly ConcurrentDictionary<Guid, Meal> _store = new();

    public Task<Meal> AddAsync(Meal meal, CancellationToken cancellationToken = default)
    {
        _store[meal.Id] = meal;
        return Task.FromResult(meal);
    }

    public Task<IEnumerable<Meal>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.Values.AsEnumerable());
    }

    public Task<Meal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _store.TryGetValue(id, out var meal);
        return Task.FromResult(meal);
    }
}