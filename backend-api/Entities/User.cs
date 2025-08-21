using System.ComponentModel.DataAnnotations.Schema;

namespace PaintingManager.Api.Entities;

[Table("users")]
public class User
{
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    public string Username { get; set; } = null!;

    [Column("password_hash")]
    public string PasswordHash { get; set; } = null!;

    [Column("role_id")]
    public int RoleId { get; set; }

    // Relacionamento com a tabela roles
    public Role Role { get; set; } = null!;
}
