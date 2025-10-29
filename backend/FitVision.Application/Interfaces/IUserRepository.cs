using FitVision.Domain.Entities;

namespace FitVision.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(string email);
    }
}
