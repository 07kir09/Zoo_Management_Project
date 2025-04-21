using Zoo_Management_Project.Zoo.Domain.Enums;

namespace Zoo_Management_Project.Zoo.WebApi.Dtos;

public record CreateFeedingRequest(
    Guid AnimalId, 
    TimeOnly Time, 
    FoodType FoodType);