using Microsoft.AspNetCore.Mvc;
using PaintingManager.Api.Dtos.Colors;
using PaintingManager.Api.Services;

namespace PaintingManager.Api.Controllers
{
    [ApiController]
    [Route("api/colors")]
    public class ColorsController : ControllerBase
    {
        private readonly ColorService _colorService;

        public ColorsController(ColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var colors = await _colorService.GetAllAsync();
            return Ok(colors);
        }

        [HttpGet("list/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var color = await _colorService.GetByIdAsync(id);
            if (color == null) return NotFound();
            return Ok(color);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateColorDto dto)
        {
            var newColor = await _colorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newColor.Id }, newColor);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, UpdateColorDto dto)
        {
            var updated = await _colorService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _colorService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
