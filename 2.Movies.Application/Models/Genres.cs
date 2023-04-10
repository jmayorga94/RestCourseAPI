﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Models
{
    public class Genres
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }

    }
}
