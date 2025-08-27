using Microsoft.EntityFrameworkCore;
using PaintingManager.Api.Data;
using PaintingManager.Api.Dtos.Components;
using PaintingManager.Api.Entities;

namespace PaintingManager.Api.Services
{
    public class ComponentService
    {
        private readonly AppDbContext _context;

        public ComponentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComponentDto>> GetAllAsync()
        {
            return await _context.Components
                .Select(c => new ComponentDto
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<ComponentDto?> GetByIdAsync(int id)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null) return null;

            return new ComponentDto
            {
                Id = component.Id,
                Name = component.Name,
            };
        }

        public async Task<ComponentDto> CreateAsync(CreateComponentDto dto)
        {
            var component = new Component
            {
                Name = dto.Name,
            };

            _context.Components.Add(component);
            await _context.SaveChangesAsync();

            return new ComponentDto
            {
                Id = component.Id,
                Name = component.Name,
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateComponentDto dto)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null) return false;

            component.Name = dto.Name;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null) return false;

            _context.Components.Remove(component);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
