using Zoo_Management_Project.Zoo.Domain.Enums;

namespace Zoo_Management_Project.Zoo.WebApi.Dtos;

public record CreateEnclosureRequest(
    EnclosureType Type, 
    int Capacity
    );