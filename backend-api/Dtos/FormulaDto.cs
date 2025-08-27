namespace PaintingManager.Api.Dtos.Formulas
{
    public class CreateFormulaDto
    {
        public int ColorId { get; set; }
        public bool IsBase { get; set; } = false;
        public string? ClientName { get; set; }
        public int? BaseFormulaId { get; set; }
    }

    public class UpdateFormulaDto
    {
        public bool IsBase { get; set; }
        public string? ClientName { get; set; }
        public int? BaseFormulaId { get; set; }
    }

    public class FormulaDto
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
        public bool IsBase { get; set; }
        public string? ClientName { get; set; }
        public int? BaseFormulaId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
