using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintingManager.Api.Entities
{
    [Table("groups")] 
    public class Group
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(50)]
        public string name { get; set; } = string.Empty;
    }
}
