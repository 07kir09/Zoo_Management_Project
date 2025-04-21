using System.Collections.Concurrent;
using Zoo_Management_Project.Zoo.Application.Interfaces;
using Zoo_Management_Project.Zoo.Domain;

namespace Zoo_Management_Project.Zoo.Infrastructure.Events;

public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
{
    private readonly ConcurrentDictionary<Guid, FeedingSchedule> _db = new();

    public Task AddAsync(FeedingSchedule f)
        => Task.Run(() => _db[f.Id] = f);

    public Task DeleteAsync(Guid id)
        => Task.Run(() => _db.TryRemove(id, out _));

    public Task<FeedingSchedule?> GetAsync(Guid id)
        => Task.FromResult(_db.TryGetValue(id, out var fs) ? fs : null);

    public Task<IEnumerable<FeedingSchedule>> GetAllAsync()
        => Task.FromResult<IEnumerable<FeedingSchedule>>(_db.Values);
}