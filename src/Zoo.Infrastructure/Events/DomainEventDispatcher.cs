using Zoo_Management_Project.Zoo.Application.Interfaces;
using Zoo_Management_Project.Zoo.Domain.Events;

namespace Zoo_Management_Project.Zoo.Infrastructure.Events;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    public Task DispatchAsync(IDomainEvent ev)
    {
        // simple log; hook into MediatR / message bus later
        Console.WriteLine($"Domain event: {ev.GetType().Name} at {ev.OccurredOn}");
        return Task.CompletedTask;
    }
}