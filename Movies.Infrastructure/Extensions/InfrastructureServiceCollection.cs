using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Repositories;
using Movies.Infrastructure.Context;
using Movies.Infrastructure.Repositories.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Extensions
{
    public  static class InfrastructureServiceCollection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MoviesDbContext>(options =>
            {
                options.UseSqlServer((connectionString),
                b => b.MigrationsAssembly(typeof(MoviesDbContext).Assembly.FullName));

            }, ServiceLifetime.Transient);


            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IMovieRepositoryNotAsync, MovieRepositoryNotAsync>();
            services.AddTransient<IMovieRepository, MovieRepository>();

            return services;
        }
    }
    
}
