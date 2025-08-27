using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintingManager.Api.Entities
{
    [Table("colors")]
    public class Color
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("group_id")]
        public int GroupId { get; set; }

        [Column("code")]
        public string code { get; set; } = string.Empty;

        [Column("name")]
        public string? Name { get; set; }

        public Group? Group { get; set; }
    }
}
