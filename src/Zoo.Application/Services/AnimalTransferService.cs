using Zoo_Management_Project.Zoo.Application.Interfaces;
using Zoo_Management_Project.Zoo.Domain.Events;

namespace Zoo_Management_Project.Zoo.Application.Services;

public class AnimalTransferService
{
    private readonly IAnimalRepository _animals;
    private readonly IEnclosureRepository _enclosures;
    private readonly IDomainEventDispatcher _dispatcher;

    public AnimalTransferService(IAnimalRepository animals, IEnclosureRepository enclosures,
        IDomainEventDispatcher dispatcher)
    { _animals = animals; _enclosures = enclosures; _dispatcher = dispatcher; }

    public async Task MoveAsync(Guid animalId, Guid targetEnclosureId)
    {
        var animal = await _animals.GetAsync(animalId) ?? throw new KeyNotFoundException();
        var fromEnc = await _enclosures.GetAsync(animal.EnclosureId) ?? throw new KeyNotFoundException();
        var toEnc   = await _enclosures.GetAsync(targetEnclosureId) ?? throw new KeyNotFoundException();
        if (!toEnc.HasSpace()) throw new InvalidOperationException("No free space");

        fromEnc.RemoveAnimal(animalId);
        toEnc.AddAnimal(animalId);
        animal.MoveTo(targetEnclosureId);

        await _animals.AddAsync(animal); // overwrite
        await _dispatcher.DispatchAsync(new AnimalMovedEvent(animalId, fromEnc.Id, toEnc.Id));
    }
}