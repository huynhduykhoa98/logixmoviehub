using LogixMovieApplication.WebApi.Entities;
using LogixMovieApplication.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogixMovieApplication.WebApi.Interfaces
{
    public interface IMovieService
    {
        Task<List<Movie>> ListMovie();

        Task<Movie> GetById(Guid id);
    }
}
