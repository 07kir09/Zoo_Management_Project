using Zoo_Management_Project.Zoo.Domain.Enums;

namespace Zoo_Management_Project.Zoo.Domain;

public class Enclosure
{
    public Guid Id { get; }
    public EnclosureType Type { get; }
    public int Capacity { get; }
    private readonly HashSet<Guid> _residents = new();

    public Enclosure(Guid id, EnclosureType type, int capacity)
    {
        Id = id;
        Type = type;
        Capacity = capacity;
    }

    public void AddAnimal(Guid animalId)
    {
        if (_residents.Count >= Capacity)
            throw new InvalidOperationException("Enclosure is full");
        _residents.Add(animalId);
    }

    public void RemoveAnimal(Guid animalId) => _residents.Remove(animalId);

    public int ResidentCount => _residents.Count;
    public int FreeSlots    => Capacity - _residents.Count;
    public bool HasSpace() => ResidentCount < Capacity;
}
