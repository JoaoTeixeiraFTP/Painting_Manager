using Microsoft.EntityFrameworkCore;
using PaintingManager.Api.Data;
using PaintingManager.Api.Dtos.FormulaLines;
using PaintingManager.Api.Entities;

namespace PaintingManager.Api.Services
{
    public class FormulaLineService
    {
        private readonly AppDbContext _context;

        public FormulaLineService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FormulaLineDto>> GetByFormulaAsync(int formulaId)
        {
            return await _context.FormulaLines
                .Where(fl => fl.FormulaId == formulaId)
                .Select(fl => new FormulaLineDto
                {
                    Id = fl.Id,
                    FormulaId = fl.FormulaId,
                    ComponentId = fl.ComponentId,
                    Quantity = fl.Quantity
                })
                .ToListAsync();
        }

        public async Task<FormulaLineDto> CreateAsync(int formulaId, CreateFormulaLineDto dto)
        {
            var line = new FormulaLine
            {
                FormulaId = formulaId,
                ComponentId = dto.ComponentId,
                Quantity = dto.Quantity
            };

            _context.FormulaLines.Add(line);
            await _context.SaveChangesAsync();

            return new FormulaLineDto
            {
                Id = line.Id,
                FormulaId = line.FormulaId,
                ComponentId = line.ComponentId,
                Quantity = line.Quantity
            };
        }

        public async Task<bool> UpdateAsync(int formulaId, int id, UpdateFormulaLineDto dto)
        {
            var line = await _context.FormulaLines
                .FirstOrDefaultAsync(fl => fl.Id == id && fl.FormulaId == formulaId);

            if (line == null) return false;

            line.Quantity = dto.Quantity;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int formulaId, int id)
        {
            var line = await _context.FormulaLines
                .FirstOrDefaultAsync(fl => fl.Id == id && fl.FormulaId == formulaId);

            if (line == null) return false;

            _context.FormulaLines.Remove(line);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
