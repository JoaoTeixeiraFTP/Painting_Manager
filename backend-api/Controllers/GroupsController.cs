using Microsoft.AspNetCore.Mvc;
using PaintingManager.Api.Dtos;
using PaintingManager.Api.Services;

namespace PaintingManager.Api.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupsController : ControllerBase
    {
        private readonly GroupService _service;

        public GroupsController(GroupService service)
        {
            _service = service;
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetAll()
        {
            var groups = await _service.GetAllAsync();
            return Ok(groups);
        }

        [HttpGet("list/{id}")]
        public async Task<ActionResult<GroupDto>> GetById(int id)
        {
            var group = await _service.GetByIdAsync(id);
            if (group == null) return NotFound();
            return Ok(group);
        }

        [HttpPost("NewGroup")]
        public async Task<ActionResult<GroupDto>> Create(CreateGroupDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.id }, created);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, UpdateGroupDto dto)
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
