using Microsoft.EntityFrameworkCore;
using PaintingManager.Api.Data;
using PaintingManager.Api.Dtos.Formulas;
using PaintingManager.Api.Entities;

namespace PaintingManager.Api.Services
{
    public class FormulaService
    {
        private readonly AppDbContext _context;

        public FormulaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FormulaDto>> GetAllAsync()
        {
            return await _context.Formulas
                .Select(f => new FormulaDto
                {
                    Id = f.Id,
                    ColorId = f.ColorId,
                    IsBase = f.IsBase,
                    ClientName = f.ClientName,
                    BaseFormulaId = f.BaseFormulaId,
                    CreatedAt = f.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<FormulaDto?> GetByIdAsync(int id)
        {
            var formula = await _context.Formulas.FindAsync(id);
            if (formula == null) return null;

            return new FormulaDto
            {
                Id = formula.Id,
                ColorId = formula.ColorId,
                IsBase = formula.IsBase,
                ClientName = formula.ClientName,
                BaseFormulaId = formula.BaseFormulaId,
                CreatedAt = formula.CreatedAt
            };
        }

        public async Task<FormulaDto> CreateAsync(CreateFormulaDto dto)
        {
            var formula = new Formula
            {
                ColorId = dto.ColorId,
                IsBase = dto.IsBase,
                ClientName = dto.ClientName,
                BaseFormulaId = dto.BaseFormulaId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Formulas.Add(formula);
            await _context.SaveChangesAsync();

            return new FormulaDto
            {
                Id = formula.Id,
                ColorId = formula.ColorId,
                IsBase = formula.IsBase,
                ClientName = formula.ClientName,
                BaseFormulaId = formula.BaseFormulaId,
                CreatedAt = formula.CreatedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateFormulaDto dto)
        {
            var formula = await _context.Formulas.FindAsync(id);
            if (formula == null) return false;

            formula.IsBase = dto.IsBase;
            formula.ClientName = dto.ClientName;
            formula.BaseFormulaId = dto.BaseFormulaId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var formula = await _context.Formulas.FindAsync(id);
            if (formula == null) return false;

            _context.Formulas.Remove(formula);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
