namespace BacklogManager.DTOs;

public class ReviewCreateViewModel
{
    public int GameId { get; set; }
    public string GameTitle { get; set; }
    public string GameImageUrl { get; set; }
        
    public int Rating { get; set; } = 5;
    public string? Comment { get; set; }
}