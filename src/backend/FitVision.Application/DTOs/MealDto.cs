namespace FitVision.Application.DTOs
{
    public class MealDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Calories { get; set; }
        public DateTime EatenAt { get; set; }
        public string? Notes { get; set; }
    }
}