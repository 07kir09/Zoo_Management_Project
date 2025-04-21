namespace Zoo_Management_Project.Zoo.Domain.Events;

public record FeedingTimeEvent(Guid FeedingScheduleId) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}