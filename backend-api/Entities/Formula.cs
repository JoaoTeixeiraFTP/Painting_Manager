using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintingManager.Api.Entities
{
    [Table("formulas")]
    public class Formula
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("color_id")]
        public int ColorId { get; set; }

        [ForeignKey("ColorId")]
        public Color? Color { get; set; }

        [Required]
        [Column("is_base")]
        public bool IsBase { get; set; } = false;

        [Column("client_name")]
        [MaxLength(100)]
        public string? ClientName { get; set; }

        [Column("base_formula_id")]
        public int? BaseFormulaId { get; set; }

        [ForeignKey("BaseFormulaId")]
        public Formula? BaseFormula { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("description")]
        [MaxLength(255)]
        public string? Description { get; set; }

        public ICollection<FormulaLine>? FormulaLines { get; set; }
    }
}
