using FitVision.Application.Exceptions;

namespace FitVision.Domain.Entities
{
    public class Meal
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
        public int Calories { get; private set; }
        public DateTime EatenAt { get; private set; } = DateTime.UtcNow;
        public string? Notes { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreatedAt {  get; private set; } = DateTime.UtcNow;

        public Meal(string name, int calories, Guid userId, DateTime eatenAt, string? notes = null)
        {
            Name = name;
            Calories = calories;
            EatenAt = eatenAt;
            Notes = notes;
            CreatedAt = DateTime.UtcNow;
            UserId = userId;
            Id = Guid.NewGuid();
        }

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

        public void UpdateNotes(string? notes)
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