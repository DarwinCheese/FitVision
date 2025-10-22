using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVision.Application.Commands.PartialUpdateMeal
{
    public record PartialUpdateMealCommand(Guid Id, string? Name, int? Calories, DateTime? EatenAt, string? Notes) : IRequest<Unit>;
}
