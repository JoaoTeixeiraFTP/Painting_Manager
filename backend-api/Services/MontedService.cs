using Microsoft.EntityFrameworkCore;
using PaintingManager.Api.Data;
using PaintingManager.Api.Dtos;
using PaintingManager.Api.Entities; 

namespace PaintingManager.Api.Services
{
    public class MontedService
    {
        private readonly AppDbContext _context;

        public MontedService(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Obter 1 fórmula montada pelo ID
        public MontedDto GetFormulaDetailById(int formulaId)
        {
            var formula = _context.Formulas
                .Include(f => f.Color)
                    .ThenInclude(c => c.Group)
                .Include(f => f.FormulaLines)
                    .ThenInclude(fl => fl.Component)
                .FirstOrDefault(f => f.Id == formulaId);

            if (formula == null) return null;

            return new MontedDto
            {
                Titulo = formula.Color.code,
                Grupo = formula.Color.Group.name,
                Cliente = formula.IsBase ? "Formula Base" : formula.ClientName,
                Formula = formula.FormulaLines
                    .Select(fl => new MontedLineDto
                    {
                        Componente = fl.Component.Name,
                        Quantidade = $"{fl.Quantity}L"
                    })
                    .ToList()
            };
        }

        // 🔹 Obter todas as fórmulas de um grupo
        public List<MontedDto> GetFormulasByGroup(int groupId)
        {
            var formulas = _context.Formulas
                .Include(f => f.Color)
                    .ThenInclude(c => c.Group)
                .Include(f => f.FormulaLines)
                    .ThenInclude(fl => fl.Component)
                .Where(f => f.Color.GroupId == groupId)
                .ToList();

            return formulas.Select(f => new MontedDto
            {
                Titulo = f.Color.code,
                NomeCor = f.Color.Name,
                Grupo = f.Color.Group.name,
                Cliente = f.IsBase ? "Formula Base" : f.ClientName,
                Formula = f.FormulaLines
                    .Select(fl => new MontedLineDto
                    {
                        Componente = fl.Component.Name,
                        Quantidade = $"{fl.Quantity}L"
                    })
                    .ToList()
            }).ToList();
        }

        // 🔹 Obter todas as fórmulas (sem filtro)
        public List<MontedDto> GetAllFormulas()
        {
            var formulas = _context.Formulas
                .Include(f => f.Color)
                    .ThenInclude(c => c.Group)
                .Include(f => f.FormulaLines)
                    .ThenInclude(fl => fl.Component)
                .ToList();

            return formulas.Select(f => new MontedDto
            {
                Titulo = f.Color.code,               // código da cor (ex: RAL1001)
                NomeCor = f.Color.Name,              // nome da cor (ex: Bege Areia)
                Grupo = f.Color.Group.name,          // grupo da cor (ex: RAL)
                Cliente = f.IsBase ? "Formula Base" : f.ClientName,
                Formula = f.FormulaLines
                    .Select(fl => new MontedLineDto
                    {
                        Componente = fl.Component.Name,
                        Quantidade = $"{fl.Quantity}L"
                    })
                    .ToList()
            }).ToList();
        }


        // 🔹 Criar nova fórmula montada
        public MontedDto CreateFormula(MontedCreateDto dto)
        {
            var formula = new Formula
            {
                ColorId = dto.ColorId,
                IsBase = dto.IsBase,
                ClientName = dto.IsBase ? null : dto.ClientName,
                FormulaLines = dto.Formula.Select(l => new FormulaLine
                {
                    ComponentId = l.ComponentId,
                    Quantity = l.Quantity
                }).ToList()
            };

            _context.Formulas.Add(formula);
            _context.SaveChanges();

            return GetFormulaDetailById(formula.Id);
        }

        public MontedDto CreateFullFormula(MontedFullCreateDto dto)
        {
            // 1️⃣ Grupo
            var group = _context.Groups.FirstOrDefault(g => g.name == dto.Color.GroupName);
            if (group == null)
            {
                group = new Group { name = dto.Color.GroupName };
                _context.Groups.Add(group);
                _context.SaveChanges();
            }

            // 2️⃣ Cor
            var color = _context.Colors.FirstOrDefault(c => c.code == dto.Color.Code && c.GroupId == group.id);
            if (color == null)
            {
                color = new Color
                {
                    code = dto.Color.Code,
                    Name = dto.Color.Name,
                    GroupId = group.id
                };
                _context.Colors.Add(color);
                _context.SaveChanges();
            }

            // 3️⃣ Criar FormulaLines
            var formulaLines = new List<FormulaLine>();
            foreach (var line in dto.Formula)
            {
                var component = _context.Components.FirstOrDefault(c => c.Name == line.ComponentName);
                if (component == null)
                {
                    component = new Component { Name = line.ComponentName };
                    _context.Components.Add(component);
                    _context.SaveChanges();
                }

                formulaLines.Add(new FormulaLine
                {
                    ComponentId = component.Id,
                    Quantity = line.Quantity
                });
            }

            // 4️⃣ Criar Formula
            var formula = new Formula
            {
                ColorId = color.Id,
                IsBase = dto.IsBase,
                ClientName = dto.IsBase ? null : dto.ClientName,
                FormulaLines = formulaLines
            };

            _context.Formulas.Add(formula);
            _context.SaveChanges();

            return GetFormulaDetailById(formula.Id);
        }
        
        public List<MontedDto> GetFormulasByTitulo(string titulo)
        {
            var formulas = _context.Formulas
                .Include(f => f.Color)
                    .ThenInclude(c => c.Group)
                .Include(f => f.FormulaLines)
                    .ThenInclude(fl => fl.Component)
                .Where(f => f.Color.code == titulo)
                .ToList();

            return formulas.Select(f => new MontedDto
            {
                Titulo = f.Color.code,
                NomeCor = f.Color.Name,
                Grupo = f.Color.Group.name,
                Cliente = f.IsBase ? "Formula Base" : f.ClientName,
                Formula = f.FormulaLines
                    .Select(fl => new MontedLineDto
                    {
                        Componente = fl.Component.Name,
                        Quantidade = $"{fl.Quantity}L"
                    })
                    .ToList()
            }).ToList();
        }
    }
}
