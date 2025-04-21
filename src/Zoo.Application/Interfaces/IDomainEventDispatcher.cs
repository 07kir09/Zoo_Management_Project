using Zoo_Management_Project.Zoo.Domain.Events;

namespace Zoo_Management_Project.Zoo.Application.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IDomainEvent ev);
}