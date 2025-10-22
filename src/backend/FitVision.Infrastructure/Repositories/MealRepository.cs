using FitVision.Domain.Entities;
using FitVision.Domain.Interfaces;
using FitVision.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVision.Infrastructure.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MealRepository> _logger;

        public MealRepository(AppDbContext context, ILogger<MealRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Meal> AddAsync(Meal meal, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Meals.AddAsync(meal, cancellationToken);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Meal {MealId} saved successfully.", meal.Id);
                return meal;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Meal {MealId}", meal.Id);
                throw;
            }
        }

        public async Task<IEnumerable<Meal>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Meals.ToListAsync(cancellationToken);
        }

        public async Task<Meal?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Meals.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var meal = await _context.Meals.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
            if (meal == null)
            {
                _logger.LogWarning("Meal {MealId} not found for deletion.", id);
                return;
            }

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Meal {MealId} deleted successfully.", id);
        }
    }

}
