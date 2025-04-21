using System.Collections.Concurrent;
using Zoo_Management_Project.Zoo.Application.Interfaces;
using Zoo_Management_Project.Zoo.Domain;

namespace Zoo_Management_Project.Zoo.Infrastructure.Events;

public class InMemoryEnclosureRepository : IEnclosureRepository
{
    private readonly ConcurrentDictionary<Guid, Enclosure> _db = new();
    public Task AddAsync(Enclosure e) => Task.Run(() => _db[e.Id] = e);
    public Task DeleteAsync(Guid id) => Task.Run(() => _db.TryRemove(id, out _));
    public Task<Enclosure?> GetAsync(Guid id) => Task.FromResult(_db.TryGetValue(id, out var e) ? e : null);
    public Task<IEnumerable<Enclosure>> GetAllAsync() => Task.FromResult<IEnumerable<Enclosure>>(_db.Values);
}