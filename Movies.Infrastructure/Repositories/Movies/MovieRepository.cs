using Microsoft.EntityFrameworkCore;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repositories.Movies
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesDbContext _moviesDbContext;
        public MovieRepository(MoviesDbContext movieContext) 
        {
            _moviesDbContext = movieContext;
        }
        public async Task<bool> CreateAsync(Movie movie)
        {
     
            await _moviesDbContext.Movies.AddAsync(movie);
            await _moviesDbContext.SaveChangesAsync();

            return true;
        }

        public Task<bool> DeletebyIdAsync(Guid id)
        {
            var movieInDb = _moviesDbContext.Movies.FirstOrDefault(x => x.Id == id);
            if (movieInDb != null)
            {
                _moviesDbContext.Movies.Remove(movieInDb);
            }
            return Task.FromResult(true);
        }

        public Task<bool> ExistsByIdAsync(Guid id)
        {
            var exists = true;

            var movieExists = _moviesDbContext.Movies.SingleOrDefault(x => x.Id == id);

            if (movieExists != null)
            {
                exists = false;
            }

            return Task.FromResult(exists);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            var movies = await _moviesDbContext.Movies.Include(g => g.Genres).AsNoTracking().ToListAsync();
            return movies;
        }

        public async Task<Movie?> GetByIdAsync(Guid Id)
        {
            var movie = await _moviesDbContext.Movies.Include(g => g.Genres).FirstOrDefaultAsync(x=>x.Id == Id);

            return movie;
        }

        public Task<bool> UpdateAsync(Movie movie)
        {
          
            _moviesDbContext.Movies.Update(movie);

            return Task.FromResult(true);
        }
    }
}
