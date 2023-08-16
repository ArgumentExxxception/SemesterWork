namespace HW2Sem.Entities;

public class UserCoins
{
    public int Id { get; set; }
    public string CoinName { get; set; }
    public double Count { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    
}