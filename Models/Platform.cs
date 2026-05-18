namespace BacklogManager.Models;

public class Platform
{
    public int Id { get; set; }
    public string Name { get; set; }

    
    public List<UserGame> UserGames { get; set; } = new();
}