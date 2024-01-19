using LogixMovieApplication.WebApi.Data;
using LogixMovieApplication.WebApi.Entities;
using LogixMovieApplication.WebApi.Interfaces;
using LogixMovieApplication.WebApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogixMovieApplication.WebApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;
        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Movie> GetById(Guid id)
        {
            return await _context.Movies.FirstAsync(x => x.Id == id);
        }

        public Task<List<Movie>> ListMovie()
        {
            return _context.Movies.AsNoTracking().ToListAsync();

        }
    }
}
