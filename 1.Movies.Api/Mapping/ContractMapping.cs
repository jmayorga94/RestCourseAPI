using Movies.Application.Models;
using Movies.Contracts;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.Api.Mapping;

public static class ContractMapping
{
 public static Movie MapToMovie(this CreateMovieRequest request)
 {
        return new Movie()
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            YearOfRelease = request.YearOfRelease,
            Genres = request.Genres.MapToGenre()
      };
 }

    public static Genres MapToGenre(this string genreName)
    {
        var genre = new Genres()
        {
            Name = genreName
        };

        return genre;
    }
    public static IList<Genres> MapToGenre(this IEnumerable<string> genreDtoList)
    {
        var genre = new List<Genres>();

        foreach (var dto in genreDtoList)
        {
            genre.Add(dto.MapToGenre());
        }

        return genre;
    }


    public static MovieResponse MapToResponse(this Movie movie)
 {
        return new MovieResponse()
        {
            Id = movie.Id,
            Title = movie.Title,
            YearOfRelease = movie.YearOfRelease,
            Genres = movie.Genres.Select(x=> x.Name)
   };
 }

public static MoviesResponse MapToResponse(this IEnumerable<Movie> movies)
{
   return new MoviesResponse()
   {
     Items = movies.Select(MapToResponse)
   };
}

public static Movie MapToMovie(this UpdateMovieRequest request, Guid id)
 {
     return new Movie()
      {
        Id = id,
        Title = request.Title,
        YearOfRelease = request.YearOfRelease,
        Genres = (List<Genres>)request.Genres
      };
 }

}
