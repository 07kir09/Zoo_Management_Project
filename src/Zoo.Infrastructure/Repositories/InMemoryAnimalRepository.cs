using System.Collections.Concurrent;
using Zoo_Management_Project.Zoo.Application.Interfaces;
using Zoo_Management_Project.Zoo.Domain;

namespace Zoo_Management_Project.Zoo.Infrastructure.Events;

public class InMemoryAnimalRepository : IAnimalRepository
{
    private readonly ConcurrentDictionary<Guid, Animal> _db = new();

    public Task AddAsync(Animal animal)
        => Task.Run(() => _db[animal.Id] = animal);
    public Task DeleteAsync(Guid id)
        => Task.Run(() => _db.TryRemove(id, out _));
    public Task<Animal?> GetAsync(Guid id)
        => Task.FromResult(_db.TryGetValue(id, out var a) ? a : null);
    public Task<IEnumerable<Animal>> GetAllAsync()
        => Task.FromResult<IEnumerable<Animal>>(_db.Values);
    public Task<long> CountAsync() => Task.FromResult((long)_db.Count);
}