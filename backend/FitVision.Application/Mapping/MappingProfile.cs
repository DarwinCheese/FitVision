using AutoMapper;
using FitVision.Domain.Entities;
using FitVision.Application.DTOs;
using FitVision.Application.Commands.UpdateMeal;

namespace FitVision.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Meal
        CreateMap<Meal, MealDto>().ReverseMap();
    }
}