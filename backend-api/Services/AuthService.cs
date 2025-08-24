using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using PaintingManager.Api.Data;
using PaintingManager.Api.Entities;
using PaintingManager.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace PaintingManager.Api.Services;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    // 🔑 Gerar JWT
    public string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(ClaimTypes.Role, user.Role.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // 🔑 Validação de utilizador
    public User? ValidateUser(LoginDto dto)
    {
        //Console.WriteLine($"[AuthService] Tentativa de login -> Username: {dto.Username}, Password (raw): {dto.Password}");

        // 1️⃣ Hash da password recebida
        var hashedPassword = ComputeSha256Hash(dto.Password);
        //Console.WriteLine($"[AuthService] SHA256 calculado: {hashedPassword}");

        // 2️⃣ Procurar na DB
        var user = _db.Users
            .Include(u => u.Role)  // <<<<<< adiciona isto
            .Where(u => u.Username == dto.Username && u.PasswordHash == hashedPassword)
            .FirstOrDefault();

        if (user == null)
        {
            //Console.WriteLine($"[AuthService] Falhou login: utilizador '{dto.Username}' não encontrado ou password inválida.");
        }
        else
        {
            //Console.WriteLine($"[AuthService] Login bem-sucedido -> UserId: {user.Id}, RoleId: {user.RoleId}");
        }

        return user;
    }

    // Função para gerar hash SHA256 (em hex, igual ao Supabase)
    private static string ComputeSha256Hash(string rawData)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }
}
