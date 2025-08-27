namespace PaintingManager.Api.Dtos.FormulaLines
{
    public class CreateFormulaLineDto
    {
        public int ComponentId { get; set; }
        public decimal Quantity { get; set; }
    }

    public class UpdateFormulaLineDto
    {
        public decimal Quantity { get; set; }
    }

    public class FormulaLineDto
    {
        public int Id { get; set; }
        public int FormulaId { get; set; }
        public int ComponentId { get; set; }
        public decimal Quantity { get; set; }
    }
}
