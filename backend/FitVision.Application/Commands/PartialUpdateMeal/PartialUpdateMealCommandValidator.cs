using FluentValidation;

namespace FitVision.Application.Commands.PartialUpdateMeal;

public class PartialUpdateMealCommandValidator : AbstractValidator<PartialUpdateMealCommand>
{
    public PartialUpdateMealCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        When(x => x.Name is not null, () =>
        {
            RuleFor(x => x.Name!)
                .NotEmpty()
                .MaximumLength(100);
        });

        When(x => x.Calories is not null, () =>
        {
            RuleFor(x => x.Calories!.Value)
                .GreaterThan(0)
                .LessThan(10000);
        });

        When(x => x.EatenAt is not null, () =>
        {
            RuleFor(x => x.EatenAt)
                .GreaterThan(DateTime.UtcNow);
        });
    }
}
