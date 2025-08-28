using Microsoft.AspNetCore.Mvc;
using PaintingManager.Api.Dtos;
using PaintingManager.Api.Services;

namespace PaintingManager.Api.Controllers
{
    [ApiController]
    [Route("api/monted")]
    public class MontedController : ControllerBase
    {
        private readonly MontedService _montedService;

        public MontedController(MontedService montedService)
        {
            _montedService = montedService;
        }

        [HttpGet("detail/{id}")]
        public ActionResult<MontedDto> GetFormulaDetail(int id)
        {
            var result = _montedService.GetFormulaDetailById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("group/{groupId}")]
        public ActionResult<List<MontedDto>> GetFormulasByGroup(int groupId)
        {
            var result = _montedService.GetFormulasByGroup(groupId);
            return Ok(result);
        }

        [HttpGet("list")]
        public ActionResult<List<MontedDto>> GetAllFormulas()
        {
            var result = _montedService.GetAllFormulas();
            return Ok(result);
        }

        // [HttpPost("create")]
        // public ActionResult<MontedDto> CreateFormula([FromBody] MontedCreateDto dto)
        // {
        //     var created = _montedService.CreateFormula(dto);
        //     return CreatedAtAction(nameof(GetFormulaDetail), new { id = created.Titulo }, created);
        // }

        [HttpPost("create")]
        public ActionResult<MontedDto> CreateFullFormula([FromBody] MontedFullCreateDto dto)
        {
            var created = _montedService.CreateFullFormula(dto);
            return CreatedAtAction(nameof(GetFormulaDetail), new { id = created.Titulo }, created);
        }

    }
}
