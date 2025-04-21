using Zoo_Management_Project.Zoo.Domain;

namespace Zoo_Management_Project.Zoo.Application.Interfaces;

public interface IAnimalRepository
{
    Task<Animal?> GetAsync(Guid id);
    Task<IEnumerable<Animal>> GetAllAsync();
    Task AddAsync(Animal animal);
    Task DeleteAsync(Guid id);
    Task<long> CountAsync();
}