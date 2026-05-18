namespace BacklogManager.DTOs;
public class DashboardViewModel
{
    public int TotalGamesInSystem { get; set; }
    public int MyLibraryCount { get; set; }
    public int CompletedCount { get; set; }
    public int PlayingCount { get; set; }
    
    public List<GameCreateViewModel> RecommendedGames { get; set; } = new List<GameCreateViewModel>();
}