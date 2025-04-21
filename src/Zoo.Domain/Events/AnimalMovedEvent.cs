namespace Zoo_Management_Project.Zoo.Domain.Events;

public record AnimalMovedEvent(Guid AnimalId, Guid From, Guid To) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}