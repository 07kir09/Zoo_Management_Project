using Zoo_Management_Project.Zoo.Application.Interfaces;
using Zoo_Management_Project.Zoo.Domain.Events;

namespace Zoo_Management_Project.Zoo.Application.Services;

public class FeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _repo;
    private readonly IDomainEventDispatcher _dispatcher;
    public FeedingOrganizationService(IFeedingScheduleRepository repo, IDomainEventDispatcher dispatcher)
    { _repo = repo; _dispatcher = dispatcher; }

    public async Task MarkDoneAsync(Guid scheduleId)
    {
        var fs = await _repo.GetAsync(scheduleId) ?? throw new KeyNotFoundException();
        fs.MarkDone();
        await _repo.AddAsync(fs);
        await _dispatcher.DispatchAsync(new FeedingTimeEvent(scheduleId));
    }
}