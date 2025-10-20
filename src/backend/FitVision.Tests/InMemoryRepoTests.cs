using FitVision.Domain.Entities;
using FitVision.Infrastructure.InMemory;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class InMemoryRepoTests
{
    [Fact]
    public async Task AddAndGet_ReturnsAddedMeal()
    {
        var repo = new InMemoryMealRepository();
        var meal = new Meal { Name = "Test Meal", Calories = 100 };
        await repo.AddAsync(meal);

        var allMeals = await repo.GetAllAsync();
        Assert.Contains(allMeals, m => m.Name == "Test Meal");
    }
}