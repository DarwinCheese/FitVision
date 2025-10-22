using FitVision.Domain.Entities;

namespace FitVision.Domain.Interfaces
{
    public interface IMealRepository
    {
        Task<Meal> AddAsync(Meal meal, CancellationToken cancellationToken = default);
        Task<IEnumerable<Meal>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Meal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}