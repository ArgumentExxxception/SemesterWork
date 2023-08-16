namespace HW2Sem.Entities;

public class Purchase
{
    public int Id { get; set; }
    public string? CoinName { get; set; }
    public double Price { get; set; }

    public double CoinAmount { get; set; }
    public int UserId { get; set; }
    
    public User? User{ get; set; }
}