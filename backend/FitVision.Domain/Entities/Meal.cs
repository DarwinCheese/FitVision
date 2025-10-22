using FitVision.Application.Exceptions;

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

        public void Update(string name, int calories, DateTime eatenAt, string? notes)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Meal name cannot be empty.");

            if (calories <= 0)
                throw new DomainException("Calories must be greater than zero.");

            if (eatenAt > DateTime.UtcNow)
                throw new DomainException("Date & Time cannot be in the future.");

            Name = name;
            Calories = calories;
            EatenAt = eatenAt;
            Notes = notes;
        }
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Meal name cannot be empty.");

            Name = name;
        }

        public void UpdateCalories(int calories)
        {
            if (calories <= 0)
                throw new DomainException("Calories must be greater than zero.");

            Calories = calories;
        }

        public void UpdateNotes(string notes)
        {
            Notes = notes;
        }

        public void UpdateEatenAt(DateTime eatenAt)
        {
            if (eatenAt > DateTime.UtcNow)
                throw new DomainException("Date & Time cannot be in the future.");

            EatenAt = eatenAt;
        }
    }
}