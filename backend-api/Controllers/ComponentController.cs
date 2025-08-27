using Microsoft.AspNetCore.Mvc;
using PaintingManager.Api.Dtos.Components;
using PaintingManager.Api.Services;

namespace PaintingManager.Api.Controllers
{
    [ApiController]
    [Route("api/component")]
    public class ComponentsController : ControllerBase
    {
        private readonly ComponentService _service;

        public ComponentsController(ComponentService service)
        {
            _service = service;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var components = await _service.GetAllAsync();
            return Ok(components);
        }

        [HttpGet("list/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var component = await _service.GetByIdAsync(id);
            if (component == null) return NotFound();
            return Ok(component);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateComponentDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, UpdateComponentDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
