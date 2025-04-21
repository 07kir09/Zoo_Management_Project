using Zoo_Management_Project.Zoo.Domain.Enums;

namespace Zoo_Management_Project.Zoo.WebApi.Dtos;

public record EnclosureDto(
    Guid Id, 
    EnclosureType Type, 
    int Capacity, 
    int ResidentCount, 
    int FreeSlots
    );