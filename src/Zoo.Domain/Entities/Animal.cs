using Zoo_Management_Project.Zoo.Domain.Enums;

namespace Zoo_Management_Project.Zoo.Domain;

public class Animal
{
    public Guid Id { get; }
    public string Species    { get; }
    public string Name       { get; }
    public DateTime BirthDate{ get; }
    public Gender Gender     { get; private set; }
    public FoodType FavoriteFood { get; private set; }
    public HealthStatus Status   { get; private set; }
    public Guid EnclosureId      { get; private set; }

    // Новое свойство для времени последнего кормления
    public DateTime? LastFedTime { get; private set; }

    // Обратите внимание: добавляем параметр enclosureId
    public Animal(Guid id, string species, string name, DateTime birthDate,
        Gender gender, FoodType favoriteFood, HealthStatus status, Guid enclosureId)
    {
        Id            = id;
        Species       = species;
        Name          = name;
        BirthDate     = birthDate;
        Gender        = gender;
        FavoriteFood  = favoriteFood;
        Status        = status;
        EnclosureId   = enclosureId;
    }

    // Реализация кормления
    public void Feed()
    {
        LastFedTime = DateTime.UtcNow;
    }

    public void Treat()
    {
        Status = HealthStatus.Healthy;
    }

    public void MoveTo(Guid newEnclosure)
    {
        EnclosureId = newEnclosure;
    }
}