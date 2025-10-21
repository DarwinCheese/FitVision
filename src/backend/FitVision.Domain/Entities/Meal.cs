namespace FitVision.Domain.Entities
{
    public class Meal
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public int Calories { get; set; }
        public DateTime EatenAt { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt {  get; set; } = DateTime.UtcNow;
    }
}