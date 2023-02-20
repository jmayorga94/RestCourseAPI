namespace Movies.Application.Models;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int YearOfRelease { get; set; }
    public List<string> Genres { get; set; } = new ();
    
}
