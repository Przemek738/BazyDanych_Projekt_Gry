namespace BacklogManager.Models;

public class UserGame
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }

    public int GameId { get; set; }
    public Game Game { get; set; }
    public List<Platform> Platforms { get; set; } = new();
    
    public string Status { get; set; }
    public int? Rating { get; set; }
    public string? Notes { get; set; }
}