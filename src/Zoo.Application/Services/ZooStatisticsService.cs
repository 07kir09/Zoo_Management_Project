using Zoo_Management_Project.Zoo.Application.Interfaces;
using Zoo_Management_Project.Zoo.Application.Models;

namespace Zoo_Management_Project.Zoo.Application.Services;

public class ZooStatisticsService
{
    private readonly IAnimalRepository _animals; private readonly IEnclosureRepository _enclosures;
    public ZooStatisticsService(IAnimalRepository a, IEnclosureRepository e)
    { _animals = a; _enclosures = e; }

    public async Task<ZooStats> SnapshotAsync()
    {
        var encl = await _enclosures.GetAllAsync();
        var animalsCnt = await _animals.CountAsync();
        var free = encl.Count(e => e.HasSpace());
        return new ZooStats(animalsCnt, encl.LongCount(), free);
    }
}