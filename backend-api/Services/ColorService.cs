using Microsoft.EntityFrameworkCore;
using PaintingManager.Api.Data;
using PaintingManager.Api.Dtos.Colors;
using PaintingManager.Api.Entities;

namespace PaintingManager.Api.Services
{
    public class ColorService
    {
        private readonly AppDbContext _context;

        public ColorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ColorDto>> GetAllAsync()
        {
            return await _context.Colors
                .Select(c => new ColorDto
                {
                    Id = c.Id,
                    GroupId = c.GroupId,
                    code = c.code,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<ColorDto?> GetByIdAsync(int id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color == null) return null;

            return new ColorDto
            {
                Id = color.Id,
                GroupId = color.GroupId,
                code = color.code,
                Name = color.Name
            };
        }

        public async Task<ColorDto> CreateAsync(CreateColorDto dto)
        {
            var color = new Color
            {
                GroupId = dto.GroupId,
                code = dto.code,
                Name = dto.Name
            };

            _context.Colors.Add(color);
            await _context.SaveChangesAsync();

            return new ColorDto
            {
                Id = color.Id,
                GroupId = color.GroupId,
                code = color.code,
                Name = color.Name
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateColorDto dto)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color == null) return false;

            color.GroupId = dto.GroupId;
            color.code = dto.code;
            color.Name = dto.Name;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color == null) return false;

            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
