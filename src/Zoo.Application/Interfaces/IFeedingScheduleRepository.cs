using Zoo_Management_Project.Zoo.Domain;

namespace Zoo_Management_Project.Zoo.Application.Interfaces;

public interface IFeedingScheduleRepository {
    Task<FeedingSchedule?> GetAsync(Guid id);
    Task<IEnumerable<FeedingSchedule>> GetAllAsync();
    Task AddAsync(FeedingSchedule f);
    Task DeleteAsync(Guid id);
}