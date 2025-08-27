using Microsoft.AspNetCore.Mvc;
using PaintingManager.Api.Dtos.FormulaLines;
using PaintingManager.Api.Services;

namespace PaintingManager.Api.Controllers
{
    [ApiController]
    [Route("api/formulas/{formulaId}/formulaline")]
    public class FormulaLinesController : ControllerBase
    {
        private readonly FormulaLineService _service;

        public FormulaLinesController(FormulaLineService service)
        {
            _service = service;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll(int formulaId)
        {
            var lines = await _service.GetByFormulaAsync(formulaId);
            return Ok(lines);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(int formulaId, CreateFormulaLineDto dto)
        {
            var created = await _service.CreateAsync(formulaId, dto);
            return CreatedAtAction(nameof(GetAll), new { formulaId }, created);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int formulaId, int id, UpdateFormulaLineDto dto)
        {
            var updated = await _service.UpdateAsync(formulaId, id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int formulaId, int id)
        {
            var deleted = await _service.DeleteAsync(formulaId, id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
