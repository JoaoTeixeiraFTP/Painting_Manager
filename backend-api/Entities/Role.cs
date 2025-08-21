using System.ComponentModel.DataAnnotations.Schema;

namespace PaintingManager.Api.Entities;

[Table("roles")]
public class Role
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;
}
