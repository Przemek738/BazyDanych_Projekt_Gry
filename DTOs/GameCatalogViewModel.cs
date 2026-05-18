namespace BacklogManager.DTOs;
public class GameCatalogViewModel
{
    public IEnumerable<GameCreateViewModel> Games { get; set; }
    
    public List<string> AvailableGenres { get; set; }
    
    public string SearchString { get; set; }
    
    public string SelectedGenre { get; set; }
    
    public bool IsInLibrary { get; set; }
}