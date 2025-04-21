using Zoo_Management_Project.Zoo.Domain.Enums;

namespace Zoo_Management_Project.Zoo.Domain;

public class FeedingSchedule
{
    public Guid Id { get; }
    public Guid AnimalId { get; }
    public DateTime FeedingTime { get; }
    public FoodType FoodType { get; }
    public FeedingStatus Status { get; private set; }

    public FeedingSchedule(Guid id, Guid animalId, DateTime feedingTime, FoodType foodType)
    {
        Id = id;
        AnimalId = animalId;
        FeedingTime = feedingTime;
        FoodType = foodType;
        Status = FeedingStatus.Scheduled;
    }

    public void MarkDone()     => Status = FeedingStatus.Completed;
    public void MarkMissed()   => Status = FeedingStatus.Missed;
}