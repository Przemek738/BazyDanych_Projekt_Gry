using BacklogManager.Models;

namespace BacklogManager.DTOs;
public class AddToLibraryViewModel
{
    public int GameId { get; set; }
    public string GameTitle { get; set; }

    public List<int> SelectedPlatformIds { get; set; } = new();
    public List<Platform> AvailablePlatforms { get; set; } = new();

    public string Status { get; set; } = "Planuję zagrać";
    public string? Notes { get; set; }
}