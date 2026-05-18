namespace BacklogManager.DTOs;
public class GameEditViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Developer { get; set; }
    public int ReleaseYear { get; set; }
    public string Genre { get; set; }
    public string ImageUrl { get; set; }
}