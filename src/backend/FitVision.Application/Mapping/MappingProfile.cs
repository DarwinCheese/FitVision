using AutoMapper;
using FitVision.Domain.Entities;
using FitVision.Application.DTOs;

namespace FitVision.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Meal, MealDto>().ReverseMap();
    }
}