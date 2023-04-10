﻿using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public interface IMovieService
    {
        Task<bool> CreateAsync(Movie movie);
        Task<Movie?> GetByIdAsync(Guid Id);

        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> UpdateAsync(Movie movie);
        Task<bool> DeletebyIdAsync(Guid id);

    }
}
