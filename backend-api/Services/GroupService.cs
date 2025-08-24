using Microsoft.EntityFrameworkCore;
using PaintingManager.Api.Data;
using PaintingManager.Api.Dtos;
using PaintingManager.Api.Entities;

namespace PaintingManager.Api.Services
{
    public class GroupService
    {
        private readonly AppDbContext _context;

        public GroupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupDto>> GetAllAsync()
        {
            return await _context.Groups
                .Select(g => new GroupDto { id = g.id, name = g.name })
                .ToListAsync();
        }

        public async Task<GroupDto?> GetByIdAsync(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return null;

            return new GroupDto { id = group.id, name = group.name };
        }

        public async Task<GroupDto> CreateAsync(CreateGroupDto dto)
        {
            var group = new Group { name = dto.name };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return new GroupDto { id = group.id, name = group.name };
        }

        public async Task<bool> UpdateAsync(int id, UpdateGroupDto dto)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return false;

            group.name = dto.name;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return false;

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
