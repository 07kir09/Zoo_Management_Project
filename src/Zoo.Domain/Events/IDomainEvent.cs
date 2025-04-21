namespace Zoo_Management_Project.Zoo.Domain.Events;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}