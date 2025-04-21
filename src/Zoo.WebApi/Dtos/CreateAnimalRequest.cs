using Zoo_Management_Project.Zoo.Domain.Enums;

namespace Zoo_Management_Project.Zoo.WebApi.Dtos;

public record CreateAnimalRequest(
    string Species,
    string Name,
    DateTime BirthDate,
    Gender Gender,
    FoodType FavouriteFood,
    Guid EnclosureId
);