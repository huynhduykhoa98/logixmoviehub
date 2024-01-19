using LogixMovieApplication.WebApi.Entities;
using LogixMovieApplication.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogixMovieApplication.WebApi.Interfaces
{
    public interface IUserService
    {
        Task<bool> Login(string email, string password);
        Task<User?> GetById(Guid id);
        Task<User?> GetByEmail(string email);
        Task Register(RegisterVM model);
    }
}
