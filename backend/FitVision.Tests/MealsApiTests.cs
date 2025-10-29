using FitVision.Application.Commands.CreateMeal;
using FitVision.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

public class MealsApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public MealsApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task PostAndGetMeal_ReturnsCreatedMeal()
    {
        var client = _factory.CreateClient();
        var newMeal = new CreateMealCommand("Integration Meal", 250, DateTime.UtcNow, null, DateTime.UtcNow, Guid.NewGuid());

        var postResponse = await client.PostAsJsonAsync("/api/meals", newMeal);
        postResponse.EnsureSuccessStatusCode();

        var getResponse = await client.GetFromJsonAsync<MealDto[]>("/api/meals");
        Assert.NotNull(getResponse); // Ensure getResponse is not null before passing to Assert.Contains
        Assert.Contains(getResponse, m => m.Name == "Integration Meal");
    }
}