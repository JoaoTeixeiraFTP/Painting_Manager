using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintingManager.Api.Entities
{
    [Table("formula_lines")]
    public class FormulaLine
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("formula_id")]
        public int FormulaId { get; set; }

        [ForeignKey("FormulaId")]
        public Formula? Formula { get; set; }

        [Required]
        [Column("component_id")]
        public int ComponentId { get; set; }

        [ForeignKey("ComponentId")]
        public Component? Component { get; set; }

        [Required]
        [Column("quantity", TypeName = "numeric(10,2)")]
        public decimal Quantity { get; set; }
    }
}
