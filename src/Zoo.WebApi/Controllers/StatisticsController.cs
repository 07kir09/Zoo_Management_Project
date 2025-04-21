using Microsoft.AspNetCore.Mvc;
using Zoo_Management_Project.Zoo.Application.Models;
using Zoo_Management_Project.Zoo.Application.Services;

namespace Zoo_Management_Project.Zoo.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly ZooStatisticsService _stats;

    public StatisticsController(ZooStatisticsService stats)
        => _stats = stats;

    [HttpGet]
    public async Task<ZooStats> GetSnapshot()
        => await _stats.SnapshotAsync();
}