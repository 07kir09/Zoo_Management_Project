using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zoo_Management_Project.Zoo.Application.Interfaces;
using Zoo_Management_Project.Zoo.Domain;
using Zoo_Management_Project.Zoo.WebApi.Dtos;

namespace Zoo_Management_Project.Zoo.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnclosuresController : ControllerBase
    {
        private readonly IEnclosureRepository _repo;
        public EnclosuresController(IEnclosureRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IEnumerable<EnclosureDto>> GetAll()
        {
            var all = await _repo.GetAllAsync();    // <-- убеждаемся, что метод называется именно так
            return all
                .Select(e => new EnclosureDto(
                    e.Id,
                    e.Type,
                    e.Capacity,
                    e.ResidentCount,
                    e.FreeSlots));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEnclosureRequest r)
        {
            var enc = new Enclosure(Guid.NewGuid(), r.Type, r.Capacity);
            await _repo.AddAsync(enc);
            var dto = new EnclosureDto(
                enc.Id,
                enc.Type,
                enc.Capacity,
                enc.ResidentCount,
                enc.FreeSlots);
            return CreatedAtAction(nameof(GetAll), new { id = enc.Id }, dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}