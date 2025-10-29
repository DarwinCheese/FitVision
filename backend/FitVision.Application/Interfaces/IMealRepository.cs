using FitVision.Domain.Entities;

namespace FitVision.Application.Interfaces
{
    public interface IMealRepository
    {
        Task<Meal> AddAsync(Meal meal, CancellationToken cancellationToken = default);
        Task<IEnumerable<Meal>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Meal>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<Meal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(Meal meal);
    }
}