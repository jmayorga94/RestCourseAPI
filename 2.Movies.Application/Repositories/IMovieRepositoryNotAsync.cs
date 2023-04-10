using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public interface IMovieRepositoryNotAsync
    {
        bool Create(Movie movie);
        Movie GetById(Guid Id);
        IEnumerable<Movie> GetAll();
        bool Update(Movie movie);
        bool DeletebyId(Guid id);
    }
}
