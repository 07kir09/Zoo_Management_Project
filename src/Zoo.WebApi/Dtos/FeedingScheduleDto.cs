using Zoo_Management_Project.Zoo.Domain.Enums;

namespace Zoo_Management_Project.Zoo.WebApi.Dtos;

public record FeedingScheduleDto(
    Guid Id, 
    Guid AnimalId, 
    TimeOnly FeedingTime, 
    FoodType FoodType, 
    FeedingStatus Status
    );