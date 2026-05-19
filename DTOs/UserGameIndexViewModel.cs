namespace BacklogManager.DTOs;
public class UserGameIndexViewModel
{
    public int UserGameId { get; set; }
    public int GameId { get; set; }
    public string GameTitle { get; set; }
    public List<string> PlatformNames { get; set; } = new();
    public string Status { get; set; }
    public int? Rating { get; set; }
    public string? ImageUrl { get; set; }
    public string? Notes { get; set; }
}