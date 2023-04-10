using Microsoft.EntityFrameworkCore;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repositories.Movies
{
    public class MovieRepositoryNotAsync : IMovieRepositoryNotAsync
    {
        private readonly MoviesDbContext _moviesDbContext;
        public MovieRepositoryNotAsync(MoviesDbContext movieContext)
        {
            _moviesDbContext = movieContext;
        }
        public bool Create(Movie movie)
        {
             _moviesDbContext.Movies.AddAsync(movie);
             _moviesDbContext.SaveChangesAsync();

            return true;
        }

        public bool DeletebyId(Guid id)
        {
            var movieInDb = _moviesDbContext.Movies.FirstOrDefault(x => x.Id == id);
            if (movieInDb != null)
            {
                _moviesDbContext.Movies.Remove(movieInDb);
            }
            return true;
        }

        public IEnumerable<Movie> GetAll()
        {
            var movies =  _moviesDbContext.Movies.Include(g => g.Genres).ToList();
            return movies;
        }

        public Movie GetById(Guid Id)
        {
            var movie = _moviesDbContext.Movies.Include(g => g.Genres).FirstOrDefault(x => x.Id == Id);

            return movie;
        }

        public bool Update(Movie movie)
        {
            _moviesDbContext.Movies.Update(movie);

            return true;
        }
    }
}
