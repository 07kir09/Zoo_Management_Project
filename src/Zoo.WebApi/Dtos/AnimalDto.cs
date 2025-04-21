using Zoo_Management_Project.Zoo.Domain.Enums;

namespace Zoo_Management_Project.Zoo.WebApi.Dtos;

public record AnimalDto(
    Guid Id,
    string Species,
    string Name,
    DateTime BirthDate,
    Gender Gender,
    FoodType FavouriteFood,
    HealthStatus Status,
    Guid EnclosureId
);