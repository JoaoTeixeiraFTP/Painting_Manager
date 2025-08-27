using Microsoft.AspNetCore.Mvc;
using PaintingManager.Api.Dtos.Formulas;
using PaintingManager.Api.Services;

namespace PaintingManager.Api.Controllers
{
    [ApiController]
    [Route("api/formula")]
    public class FormulasController : ControllerBase
    {
        private readonly FormulaService _service;

        public FormulasController(FormulaService service)
        {
            _service = service;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var formulas = await _service.GetAllAsync();
            return Ok(formulas);
        }

        [HttpGet("list/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var formula = await _service.GetByIdAsync(id);
            if (formula == null) return NotFound();
            return Ok(formula);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateFormulaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, UpdateFormulaDto dto)
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
