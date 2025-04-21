using Zoo_Management_Project.Zoo.Domain;

namespace Zoo_Management_Project.Zoo.Application.Interfaces;

public interface IEnclosureRepository
{
    Task<IEnumerable<Enclosure>> GetAllAsync();

    Task<Enclosure?> GetAsync(Guid id);
    
    Task AddAsync(Enclosure enclosure);
    Task DeleteAsync(Guid id);
}