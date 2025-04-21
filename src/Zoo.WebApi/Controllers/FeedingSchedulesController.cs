using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zoo_Management_Project.Zoo.Domain.Enums;
using Zoo_Management_Project.Zoo.Application.Interfaces;
using Zoo_Management_Project.Zoo.Application.Services;
using Zoo_Management_Project.Zoo.Domain;
using Zoo_Management_Project.Zoo.WebApi.Dtos;

namespace Zoo_Management_Project.Zoo.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedingSchedulesController : ControllerBase
    {
        private readonly IFeedingScheduleRepository _repo;
        private readonly FeedingOrganizationService _service;

        public FeedingSchedulesController(
            IFeedingScheduleRepository repo,
            FeedingOrganizationService service)
        {
            _repo = repo;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedingScheduleDto>>> GetAll()
        {
            var all = await _repo.GetAllAsync();
            var dtos = all.Select(f => new FeedingScheduleDto(
                f.Id,
                f.AnimalId,
                TimeOnly.FromDateTime(f.FeedingTime),
                f.FoodType,
                f.Status
            ));
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<FeedingScheduleDto>> Create([FromBody] CreateFeedingRequest r)
        {
            var dateTime = DateTime.Today.Add(r.Time.ToTimeSpan());

            var fs = new FeedingSchedule(
                Guid.NewGuid(),
                r.AnimalId,
                dateTime,     // теперь передаём корректный DateTime
                r.FoodType
            );
            await _repo.AddAsync(fs);

            var dto = new FeedingScheduleDto(
                fs.Id,
                fs.AnimalId,
                TimeOnly.FromDateTime(fs.FeedingTime),
                fs.FoodType,
                fs.Status
            );
            return CreatedAtAction(nameof(GetAll), new { id = fs.Id }, dto);
        }


        [HttpPost("{id}/done")]
        public async Task<IActionResult> Done(Guid id)
        {
            await _service.MarkDoneAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}