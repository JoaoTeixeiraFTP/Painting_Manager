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
                Id = formula.Id,
                Titulo = formula.Color.code,
                NomeCor = formula.Color.Name,
                Grupo = formula.Color.Group.name,
                Cliente = formula.IsBase ? "Formula Base" : formula.ClientName,
                Descricao = formula.Description, // 🔹 incluir descrição
                Formula = formula.FormulaLines
                    .Select(fl => new MontedLineDto
                    {
                        Componente = fl.Component.Name,
                        Quantidade = $"{fl.Quantity}",
                        Unit = fl.Unit
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
                Id = f.Id,
                Titulo = f.Color.code,
                NomeCor = f.Color.Name,
                Grupo = f.Color.Group.name,
                Cliente = f.IsBase ? "Formula Base" : f.ClientName,
                Descricao = f.Description, // 🔹 incluir descrição
                Formula = f.FormulaLines
                    .Select(fl => new MontedLineDto
                    {
                        Componente = fl.Component.Name,
                        Quantidade = $"{fl.Quantity}",
                        Unit = fl.Unit
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
                Id = f.Id,
                Titulo = f.Color.code,
                NomeCor = f.Color.Name,
                Grupo = f.Color.Group.name,
                Cliente = f.IsBase ? "Formula Base" : f.ClientName,
                Descricao = f.Description, // 🔹 incluir descrição
                Formula = f.FormulaLines
                    .Select(fl => new MontedLineDto
                    {
                        Componente = fl.Component.Name,
                        Quantidade = $"{fl.Quantity}",
                        Unit = fl.Unit
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
                Description = dto.Description, // 🔹 incluir descrição
                FormulaLines = dto.Formula.Select(l => new FormulaLine
                {
                    ComponentId = l.ComponentId,
                    Quantity = l.Quantity,
                    Unit = string.IsNullOrWhiteSpace(l.Unit) ? "L" : l.Unit
                }).ToList()
            };

            _context.Formulas.Add(formula);
            _context.SaveChanges();

            return GetFormulaDetailById(formula.Id);
        }

        // 🔹 Criar fórmula completa (novo grupo, cor, componentes)
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
                    Quantity = line.Quantity,
                    Unit = string.IsNullOrWhiteSpace(line.Unit) ? "L" : line.Unit
                });
            }

            // 4️⃣ Criar Formula
            var formula = new Formula
            {
                ColorId = color.Id,
                IsBase = dto.IsBase,
                ClientName = dto.IsBase ? null : dto.ClientName,
                Description = dto.Description, // 🔹 incluir descrição
                FormulaLines = formulaLines
            };

            _context.Formulas.Add(formula);
            _context.SaveChanges();

            return GetFormulaDetailById(formula.Id);
        }

        // 🔹 Obter fórmulas pelo código da cor
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
                Id = f.Id,
                Titulo = f.Color.code,
                NomeCor = f.Color.Name,
                Grupo = f.Color.Group.name,
                Cliente = f.IsBase ? "Formula Base" : f.ClientName,
                Descricao = f.Description, // 🔹 incluir descrição
                Formula = f.FormulaLines
                    .Select(fl => new MontedLineDto
                    {
                        Componente = fl.Component.Name,
                        Quantidade = $"{fl.Quantity}",
                        Unit = fl.Unit
                    })
                    .ToList()
            }).ToList();
        }
        
        // 🔹 Atualizar fórmula existente
        public MontedDto UpdateFormula(int formulaId, MontedFullCreateDto dto)
        {
            var formula = _context.Formulas
                .Include(f => f.FormulaLines)
                .Include(f => f.Color) 
                .FirstOrDefault(f => f.Id == formulaId);

            if (formula == null)
                throw new KeyNotFoundException($"Fórmula com ID {formulaId} não encontrada.");

            var group = _context.Groups.FirstOrDefault(g => g.name == dto.Color.GroupName);
            if (group == null)
            {
                group = new Group { name = dto.Color.GroupName };
                _context.Groups.Add(group);
                _context.SaveChanges();
            }

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
            else
            {
                color.Name = dto.Color.Name;
                _context.SaveChanges();
            }

            formula.ColorId = color.Id;
            formula.IsBase = dto.IsBase;
            formula.ClientName = dto.IsBase ? null : dto.ClientName;
            formula.Description = dto.Description; // 🔹 atualizar descrição

            _context.FormulaLines.RemoveRange(formula.FormulaLines);

            var newLines = new List<FormulaLine>();
            foreach (var line in dto.Formula)
            {
                var component = _context.Components.FirstOrDefault(c => c.Name == line.ComponentName);
                if (component == null)
                {
                    component = new Component { Name = line.ComponentName };
                    _context.Components.Add(component);
                    _context.SaveChanges();
                }

                newLines.Add(new FormulaLine
                {
                    ComponentId = component.Id,
                    Quantity = line.Quantity,
                    Unit = string.IsNullOrWhiteSpace(line.Unit) ? "L" : line.Unit
                });
            }

            formula.FormulaLines = newLines;
            _context.SaveChanges();

            return GetFormulaDetailById(formula.Id);
        }
    }
}
