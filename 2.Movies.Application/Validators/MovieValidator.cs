using FluentValidation;
using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Validators
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        public MovieValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("El id no debe estar vacio");
            RuleFor(x => x.Genres).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().WithMessage("El titulo no debe estar vacio");
            RuleFor(x => x.YearOfRelease).LessThanOrEqualTo(DateTime.UtcNow.Year);
             
        }
    }
}
