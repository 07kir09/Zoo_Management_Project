using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zoo_Management_Project.Zoo.Application.Interfaces;
using Zoo_Management_Project.Zoo.Application.Services;
using Zoo_Management_Project.Zoo.Domain;
using Zoo_Management_Project.Zoo.Domain.Enums;
using Zoo_Management_Project.Zoo.WebApi.Dtos;

namespace Zoo_Management_Project.Zoo.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalRepository _repo;
        private readonly AnimalTransferService _transfer;

        public AnimalsController(IAnimalRepository repo, AnimalTransferService transfer)
        {
            _repo = repo;
            _transfer = transfer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalDto>>> GetAll()
        {
            var all = await _repo.GetAllAsync();
            var dtos = all.Select(a => new AnimalDto(
                a.Id,
                a.Species,
                a.Name,
                a.BirthDate,
                a.Gender,
                a.FavoriteFood,    // <- исправлено
                a.Status,
                a.EnclosureId
            ));
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<AnimalDto>> Create([FromBody] CreateAnimalRequest r)
        {
            var animal = new Animal(
                Guid.NewGuid(),
                r.Species,
                r.Name,
                r.BirthDate,
                r.Gender,
                r.FavouriteFood,
                HealthStatus.Healthy,
                r.EnclosureId
            );
            await _repo.AddAsync(animal);

            var dto = new AnimalDto(
                animal.Id,
                animal.Species,
                animal.Name,
                animal.BirthDate,
                animal.Gender,
                animal.FavoriteFood,
                animal.Status,
                animal.EnclosureId
            );
            return CreatedAtAction(nameof(GetAll), new { id = animal.Id }, dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/move/{enclosureId}")]
        public async Task<IActionResult> Move(Guid id, Guid enclosureId)
        {
            await _transfer.MoveAsync(id, enclosureId);
            return NoContent();
        }
    }
}