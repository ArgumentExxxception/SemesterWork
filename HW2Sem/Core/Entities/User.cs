using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HW2Sem.Models;

namespace HW2Sem.Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    public double Balance { get; set; }
    public string Role { get; set; }

    public List<Comment>? Comments { get; set; }
    public List<UserAuthor>? Authors { get; set; }
    public List<Purchase> Purchases { get; set; }

    public bool ValidatePassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password,Password);
    }

    public List<UserCoins?> UserCoins { get; set; }

}